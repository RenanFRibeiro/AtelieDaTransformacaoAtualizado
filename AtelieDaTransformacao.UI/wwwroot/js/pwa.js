// Registro do Service Worker + botão de instalação do PWA.
(function () {
  "use strict";

  if ("serviceWorker" in navigator) {
    window.addEventListener("load", () => {
      navigator.serviceWorker
        .register("/sw.js")
        .catch((err) => console.warn("Falha ao registrar o service worker:", err));
    });
  }

  // ---------------------------------------------------------------------
  // Botão flutuante "Instalar app" — só aparece quando o navegador
  // sinaliza que a instalação é possível (Chrome/Edge/Android).
  // ---------------------------------------------------------------------
  let deferredPrompt = null;

  function createInstallButton() {
    const btn = document.createElement("button");
    btn.id = "pwaInstallButton";
    btn.type = "button";
    btn.setAttribute("aria-label", "Instalar aplicativo do Ateliê da Transformação");
    btn.title = "Instalar app";
    btn.innerHTML = '<i class="bi bi-download" aria-hidden="true"></i><span>Instalar app</span>';
    btn.style.cssText = [
      "position:fixed",
      "left:20px",
      "bottom:20px",
      "z-index:1040",
      "display:none",
      "align-items:center",
      "gap:.5rem",
      "padding:.65rem 1.1rem",
      "border:none",
      "border-radius:999px",
      "background:linear-gradient(135deg,#c58a58,#a85c3d)",
      "color:#241812",
      "font-weight:600",
      "font-size:.85rem",
      "box-shadow:0 8px 20px rgba(36,24,18,.35)",
      "cursor:pointer"
    ].join(";");

    btn.addEventListener("click", async () => {
      if (!deferredPrompt) return;
      btn.style.display = "none";
      deferredPrompt.prompt();
      await deferredPrompt.userChoice;
      deferredPrompt = null;
    });

    document.body.appendChild(btn);
    return btn;
  }

  window.addEventListener("beforeinstallprompt", (event) => {
    event.preventDefault();
    deferredPrompt = event;

    const existing = document.getElementById("pwaInstallButton");
    const btn = existing || createInstallButton();
    btn.style.display = "inline-flex";
  });

  window.addEventListener("appinstalled", () => {
    deferredPrompt = null;
    const btn = document.getElementById("pwaInstallButton");
    if (btn) btn.remove();
  });
})();
