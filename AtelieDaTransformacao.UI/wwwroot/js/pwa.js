// PWA: registro do Service Worker e instalação segura, acessível e responsiva.
(function () {
  "use strict";

  const isStandalone = () =>
    window.matchMedia?.("(display-mode: standalone)").matches ||
    window.navigator.standalone === true;

  const isIOS = /iphone|ipad|ipod/i.test(window.navigator.userAgent || "");
  let deferredPrompt = null;

  if ("serviceWorker" in navigator) {
    window.addEventListener("load", () => {
      navigator.serviceWorker.register("/sw.js", { updateViaCache: "none" })
        .catch((err) => console.warn("Falha ao registrar o service worker:", err));
    });
  }

  function createInstallButton() {
    const btn = document.createElement("button");
    btn.id = "pwaInstallButton";
    btn.type = "button";
    btn.className = "pwa-install-button";
    btn.setAttribute("aria-label", "Instalar aplicativo do Ateliê da Transformação");
    btn.title = "Instalar o Ateliê no celular";
    btn.innerHTML = '<i class="bi bi-phone" aria-hidden="true"></i><span class="pwa-install-label">Instalar app</span>';

    btn.addEventListener("click", async () => {
      if (!deferredPrompt) return;
      btn.disabled = true;
      deferredPrompt.prompt();
      try {
        await deferredPrompt.userChoice;
      } finally {
        deferredPrompt = null;
        btn.remove();
      }
    });

    document.body.appendChild(btn);
    return btn;
  }

  function showAndroidInstallPrompt(event) {
    event.preventDefault();
    deferredPrompt = event;
    if (isStandalone()) return;

    const btn = document.getElementById("pwaInstallButton") || createInstallButton();
    btn.hidden = false;
  }

  // Chrome/Edge/Android: o navegador fornece o prompt nativo.
  window.addEventListener("beforeinstallprompt", showAndroidInstallPrompt);

  // iOS não expõe beforeinstallprompt. Mostramos uma orientação sem pedir
  // credenciais, sem armazenar dados e sem tentar simular uma instalação.
  window.addEventListener("load", () => {
    if (!isIOS || isStandalone()) return;

    const btn = createInstallButton();
    btn.classList.add("pwa-install-button-ios");
    btn.querySelector("i").className = "bi bi-phone";
    btn.querySelector(".pwa-install-label").textContent = "Adicionar à tela inicial";
    btn.setAttribute("aria-label", "Saiba como adicionar o Ateliê à tela inicial");
    btn.title = "Adicionar à tela inicial";

    btn.addEventListener("click", () => {
      window.alert("No iPhone ou iPad: toque em Compartilhar e depois em “Adicionar à Tela de Início”.");
    }, { once: true });
  });

  window.addEventListener("appinstalled", () => {
    deferredPrompt = null;
    document.getElementById("pwaInstallButton")?.remove();
  });
})();
