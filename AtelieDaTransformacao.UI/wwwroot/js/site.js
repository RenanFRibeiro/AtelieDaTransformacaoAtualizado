document.addEventListener("DOMContentLoaded", function () {
    // Feedback visual nos inputs
    const inputs = document.querySelectorAll(".form-control, .form-select");
    inputs.forEach(input => {
        input.addEventListener("focus", () => input.classList.add("form-active"));
        input.addEventListener("blur", () => input.classList.remove("form-active"));
    });

    // Toggle do hamburguer: usa Bootstrap se disponível, senão fallback manual
    const toggler = document.querySelector('.navbar-toggler[data-bs-target="#mainNavbar"]');
    const collapseEl = document.getElementById('mainNavbar');

    if (toggler && collapseEl) {
        toggler.addEventListener('click', function () {
            if (window.bootstrap && typeof window.bootstrap.Collapse === 'function') {
                // usa a API do Bootstrap (não atrapalha se o Bootstrap já estiver funcionando)
                const instance = bootstrap.Collapse.getOrCreateInstance(collapseEl);
                instance.toggle();
            } else {
                // fallback simples
                collapseEl.classList.toggle('show');
                const expanded = this.getAttribute('aria-expanded') === 'true';
                this.setAttribute('aria-expanded', String(!expanded));
            }
        });
    }

    // Pesquisa da coleção: filtra imediatamente enquanto o usuário digita.
    const productSearch = document.getElementById("homeProductSearch");
    const productCards = Array.from(document.querySelectorAll(".product-card-modern[data-product-title]"));
    const clientSearchEmpty = document.getElementById("clientSearchEmpty");
    const collectionMetaCount = document.querySelector(".collection-meta strong");
    const collectionMeta = document.querySelector(".collection-meta");

    if (productSearch && productCards.length) {
        const normalizeText = (text) => text
            .toLocaleLowerCase("pt-BR")
            .normalize("NFD")
            .replace(/[\\u0300-\\u036f]/g, "");

        const filterProducts = () => {
            const term = normalizeText(productSearch.value.trim());
            let visibleCount = 0;

            productCards.forEach(card => {
                const title = normalizeText(card.dataset.productTitle || "");
                const matches = term === "" || title.startsWith(term);

                card.style.display = matches ? "" : "none";
                if (matches) visibleCount++;
            });

            if (collectionMetaCount) {
                collectionMetaCount.textContent = visibleCount;
            }

            if (collectionMeta) {
                collectionMeta.style.display = visibleCount > 0 ? "" : "none";
            }

            if (clientSearchEmpty) {
                clientSearchEmpty.hidden = visibleCount !== 0 || term === "";
            }
        };

        productSearch.addEventListener("input", filterProducts);
        filterProducts();
    }

});