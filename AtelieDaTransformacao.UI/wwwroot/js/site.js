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

    if (toggler && collapseEl && !(window.bootstrap && typeof window.bootstrap.Collapse === 'function')) {
        // Fallback somente quando o Bootstrap não estiver disponível.
        toggler.addEventListener('click', function () {
            collapseEl.classList.toggle('show');
            const expanded = this.getAttribute('aria-expanded') === 'true';
            this.setAttribute('aria-expanded', String(!expanded));
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

    // Tema claro/escuro: preferência persistida no navegador.
    const root = document.documentElement;
    const themeToggle = document.getElementById('themeToggle');
    const savedTheme = localStorage.getItem('atelie-theme');
    if (savedTheme === 'dark' || savedTheme === 'light') root.dataset.theme = savedTheme;

    const syncThemeIcon = () => {
        if (!themeToggle) return;
        const dark = root.dataset.theme === 'dark';
        themeToggle.innerHTML = `<i class="bi ${dark ? 'bi-sun' : 'bi-moon-stars'}" aria-hidden="true"></i>`;
        themeToggle.setAttribute('aria-label', dark ? 'Ativar modo claro' : 'Ativar modo escuro');
        themeToggle.title = dark ? 'Modo claro' : 'Modo escuro';
    };
    themeToggle?.addEventListener('click', () => {
        root.dataset.theme = root.dataset.theme === 'dark' ? 'light' : 'dark';
        localStorage.setItem('atelie-theme', root.dataset.theme);
        syncThemeIcon();
    });
    syncThemeIcon();

    document.getElementById('globalBackButton')?.addEventListener('click', () => {
        if (window.history.length > 1) window.history.back();
        else window.location.href = '/';
    });

    // Menu mobile: fecha após selecionar um link e permite ESC.
    document.querySelectorAll('#mainNavbar .nav-link, #mainNavbar .header-menu-link-v3, #mainNavbar .header-register-link')
        .forEach(link => link.addEventListener('click', () => {
            if (window.innerWidth < 992 && collapseEl && window.bootstrap)
                bootstrap.Collapse.getOrCreateInstance(collapseEl).hide();
        }));
    document.addEventListener('keydown', event => {
        if (event.key === 'Escape' && collapseEl?.classList.contains('show') && window.bootstrap)
            bootstrap.Collapse.getOrCreateInstance(collapseEl).hide();
    });

    // Biblioteca leve de animações reutilizável.
    document.querySelectorAll('.product-card-modern, .auth-card, .feedback-order-card, .order-card').forEach(el => {
        if (!el.hasAttribute('data-animate')) el.setAttribute('data-animate', 'fade');
    });

    const animatedElements = document.querySelectorAll('[data-animate]');
    if ('IntersectionObserver' in window && animatedElements.length) {
        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('is-animated');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.12 });
        animatedElements.forEach(el => observer.observe(el));
    } else {
        animatedElements.forEach(el => el.classList.add('is-animated'));
    }

    // Notificações de status em tempo real via SignalR.
    const notificationButton = document.getElementById('notificationButton');
    const notificationPanel = document.getElementById('notificationPanel');
    const notificationBadge = document.getElementById('notificationBadge');
    const notificationList = document.getElementById('notificationList');
    const clearNotifications = document.getElementById('clearNotifications');

    if (notificationButton && notificationPanel && notificationList) {
        const storageKey = 'atelie-notifications';
        let notifications = JSON.parse(localStorage.getItem(storageKey) || '[]');
        let unread = 0;

        const escapeHtml = value => String(value ?? '').replace(/[&<>"']/g, char => ({
            '&':'&amp;', '<':'&lt;', '>':'&gt;', '"':'&quot;', "'":'&#039;'
        }[char]));

        const renderNotifications = () => {
            notificationList.innerHTML = '';
            if (!notifications.length) {
                notificationList.innerHTML = '<p class="small text-muted mb-0">Nenhuma notificação nova.</p>';
            } else {
                notifications.slice(0, 8).forEach(item => {
                    const row = document.createElement('a');
                    row.className = 'notification-item';
                    row.href = item.url || '#';
                    row.innerHTML = `<strong>${escapeHtml(item.title)}</strong><span>${escapeHtml(item.text)}</span><small>${escapeHtml(item.time)}</small>`;
                    notificationList.appendChild(row);
                });
            }
            notificationBadge.hidden = unread === 0;
            notificationBadge.textContent = unread > 9 ? '9+' : unread;
        };

        notificationButton.addEventListener('click', () => {
            const opening = notificationPanel.hidden;
            notificationPanel.hidden = !opening;
            notificationButton.setAttribute('aria-expanded', String(opening));
            if (opening) {
                unread = 0;
                renderNotifications();
            }
        });

        clearNotifications?.addEventListener('click', () => {
            notifications = [];
            unread = 0;
            localStorage.removeItem(storageKey);
            renderNotifications();
        });

        renderNotifications();

        if (window.signalR) {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl('/hubs/orders')
                .withAutomaticReconnect()
                .build();

            connection.on('StatusUpdated', data => {
                const item = {
                    title: `Pedido ${data.orderNumber}`,
                    text: `Status atualizado para ${data.statusName}.`,
                    time: new Date().toLocaleString('pt-BR'),
                    url: `/Order/Details/${data.orderId}`
                };
                notifications.unshift(item);
                notifications = notifications.slice(0, 20);
                unread++;
                localStorage.setItem(storageKey, JSON.stringify(notifications));
                renderNotifications();
            });

            connection.start().catch(error => console.warn('Notificações em tempo real indisponíveis:', error));
        }
    }

});