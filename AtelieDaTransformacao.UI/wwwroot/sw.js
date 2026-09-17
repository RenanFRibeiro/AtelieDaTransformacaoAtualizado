// Service Worker — Ateliê da Transformação
// Estratégia: app-shell mínimo em cache + fallback offline.
// Páginas autenticadas (carrinho, pedidos, painel) NUNCA são cacheadas
// agressivamente: sempre tentamos a rede primeiro para não mostrar
// dados desatualizados (estoque, status de pedido, sessão etc.).

const CACHE_VERSION = "v2";
const CACHE_NAME = `atelie-shell-${CACHE_VERSION}`;
const OFFLINE_URL = "/offline.html";

// Recursos essenciais para o app funcionar minimamente offline
// (layout básico, ícones, css/js locais e a página de fallback).
const PRECACHE_URLS = [
  OFFLINE_URL,
  "/css/site.css",
  "/css/home-modern.css",
  "/js/site.js",
  "/manifest.webmanifest",
  "/images/icons/icon-192.png",
  "/images/icons/icon-512.png",
  "/favicon.ico"
];

self.addEventListener("install", (event) => {
  event.waitUntil(
    caches
      .open(CACHE_NAME)
      .then((cache) => cache.addAll(PRECACHE_URLS))
      .catch(() => {
        // Se algum recurso falhar (ex.: offline durante o install),
        // não impede a instalação do restante do app shell.
      })
      .then(() => self.skipWaiting())
  );
});

self.addEventListener("activate", (event) => {
  event.waitUntil(
    caches
      .keys()
      .then((keys) =>
        Promise.all(
          keys
            .filter((key) => key.startsWith("atelie-shell-") && key !== CACHE_NAME)
            .map((key) => caches.delete(key))
        )
      )
      .then(() => self.clients.claim())
  );
});

// Rotas que nunca devem ser servidas do cache (dados sensíveis/dinâmicos).
const NEVER_CACHE_PATTERNS = [
  /^\/Account/i,
  /^\/Cart/i,
  /^\/Order/i,
  /^\/Orders/i,
  /^\/Admin/i,
  /^\/AdminFeedback/i,
  /^\/AdminOrders/i,
  /^\/Quote/i,
  /^\/hubs\//i
];

function isNeverCache(pathname) {
  return NEVER_CACHE_PATTERNS.some((re) => re.test(pathname));
}

function isStaticAsset(pathname) {
  return (
    pathname.startsWith("/css/") ||
    pathname.startsWith("/js/") ||
    pathname.startsWith("/lib/") ||
    pathname.startsWith("/images/") ||
    pathname.startsWith("/uploads/") ||
    pathname === "/favicon.ico" ||
    pathname === "/manifest.webmanifest"
  );
}

self.addEventListener("fetch", (event) => {
  const { request } = event;

  // Só tratamos GET; POST/PUT/DELETE sempre vão direto para a rede.
  if (request.method !== "GET") return;

  const url = new URL(request.url);

  // Recursos de outra origem (CDNs, WhatsApp, fontes do Google etc.):
  // deixa o navegador cuidar normalmente, sem interceptar.
  if (url.origin !== self.location.origin) return;

  // Navegação: sempre vai para a rede. Não armazenamos HTML de páginas,
  // porque uma resposta pode conter nome, e-mail, carrinho, sessão ou outros
  // dados específicos do usuário. Em caso de perda de conexão, mostramos
  // apenas a página offline genérica.
  if (request.mode === "navigate") {
    event.respondWith(
      fetch(request).catch(() => caches.match(OFFLINE_URL))
    );
    return;
  }

  // Nunca cachear áreas sensíveis, mesmo para sub-recursos (ex.: fetch/ajax).
  if (isNeverCache(url.pathname)) {
    return;
  }

  // Assets estáticos: stale-while-revalidate (responde rápido do cache
  // e atualiza em segundo plano).
  if (isStaticAsset(url.pathname)) {
    event.respondWith(
      caches.open(CACHE_NAME).then(async (cache) => {
        const cached = await cache.match(request);
        const networkFetch = fetch(request)
          .then((response) => {
            if (response.ok) cache.put(request, response.clone());
            return response;
          })
          .catch(() => cached);
        return cached || networkFetch;
      })
    );
  }
});
