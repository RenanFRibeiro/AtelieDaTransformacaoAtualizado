// PWA — instalação somente após ação explícita do usuário.
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

  // Captura o evento do navegador, mas NÃO abre nada automaticamente.
  window.addEventListener("beforeinstallprompt", (event) => {
    event.preventDefault();
    deferredPrompt = event;
    document.documentElement.classList.add("pwa-install-available");
  });

  function showIOSInstructions() {
    window.alert(
      "Para instalar o Ateliê da Transformação no iPhone ou iPad:\n\n" +
      "1. Toque em Compartilhar no Safari.\n" +
      "2. Escolha “Adicionar à Tela de Início”.\n" +
      "3. Confirme em “Adicionar”."
    );
  }

  async function install() {
    if (isStandalone()) return { outcome: "already-installed" };

    if (deferredPrompt) {
      const promptEvent = deferredPrompt;
      deferredPrompt = null;
      document.documentElement.classList.remove("pwa-install-available");

      try {
        promptEvent.prompt();
        const result = await promptEvent.userChoice;
        return result || { outcome: "unknown" };
      } catch (error) {
        console.warn("Não foi possível abrir a instalação do PWA:", error);
        return { outcome: "error" };
      }
    }

    if (isIOS) {
      showIOSInstructions();
      return { outcome: "ios-instructions" };
    }

    window.alert(
      "A instalação ainda não está disponível neste navegador. " +
      "Se aparecer o ícone de instalação na barra do navegador, você também pode usá-lo."
    );
    return { outcome: "unavailable" };
  }

  // A instalação só acontece quando alguma ação explícita chama esta função.
  window.AteliePWA = Object.freeze({
    install,
    isStandalone: () => isStandalone()
  });

  document.addEventListener("click", (event) => {
    const trigger = event.target.closest("[data-pwa-install]");
    if (!trigger) return;

    event.preventDefault();
    if (trigger.dataset.pwaBusy === "true") return;

    trigger.dataset.pwaBusy = "true";
    Promise.resolve(install()).finally(() => {
      trigger.dataset.pwaBusy = "false";
    });
  });

  window.addEventListener("appinstalled", () => {
    deferredPrompt = null;
    document.documentElement.classList.remove("pwa-install-available");
  });
})();
