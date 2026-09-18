// Registro do Service Worker + instalação do PWA.
(function () {
  "use strict";

  if ("serviceWorker" in navigator) {
    window.addEventListener("load", () => {
      navigator.serviceWorker
        .register("/sw.js")
        .catch((err) => console.warn("Falha ao registrar o service worker:", err));
    });
  }

  let deferredPrompt = null;

  function getInstallButton() {
    return document.getElementById("pwaInstallButton");
  }

  function showInstallButton() {
    const button = getInstallButton();
    if (!button) return;

    button.hidden = false;
    button.removeAttribute("aria-hidden");
  }

  function hideInstallButton() {
    const button = getInstallButton();
    if (!button) return;

    button.hidden = true;
  }

  async function installApp() {
    const button = getInstallButton();
    if (!button || !deferredPrompt) return;

    button.disabled = true;

    try {
      deferredPrompt.prompt();
      await deferredPrompt.userChoice;
    } catch (error) {
      console.warn("Não foi possível abrir a instalação do aplicativo:", error);
    } finally {
      deferredPrompt = null;
      button.disabled = false;
      hideInstallButton();
    }
  }

  window.addEventListener("beforeinstallprompt", (event) => {
    event.preventDefault();
    deferredPrompt = event;
    showInstallButton();
  });

  window.addEventListener("appinstalled", () => {
    deferredPrompt = null;
    hideInstallButton();
  });

  document.addEventListener("click", (event) => {
    const button = event.target.closest("#pwaInstallButton");
    if (!button) return;
    installApp();
  });

  // Se o site já estiver rodando como aplicativo instalado, não mostramos o botão.
  if (window.matchMedia("(display-mode: standalone)").matches || window.navigator.standalone === true) {
    hideInstallButton();
  }
})();
