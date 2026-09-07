document.addEventListener("DOMContentLoaded", () => {
    "use strict";

    const root = document.documentElement;

    // ---------------------------------------------------------------------
    // Inputs
    // ---------------------------------------------------------------------
    document.querySelectorAll(".form-control, .form-select").forEach(input => {
        input.addEventListener("focus", () => input.classList.add("form-active"));
        input.addEventListener("blur", () => input.classList.remove("form-active"));
    });

    // ---------------------------------------------------------------------
    // Tema: preferência por usuário/navegador, sem piscar a página.
    // ---------------------------------------------------------------------
    const themeToggle = document.getElementById("themeToggle");
    const safeStorageGet = key => { try { return localStorage.getItem(key); } catch { return null; } };
    const safeStorageSet = (key, value) => { try { localStorage.setItem(key, value); } catch { /* storage indisponível */ } };

    const syncThemeIcon = () => {
        const dark = root.dataset.theme === "dark";
        root.dataset.bsTheme = dark ? "dark" : "light";
        root.style.colorScheme = dark ? "dark" : "light";
        const icon = themeToggle.querySelector("i");
        if (icon) icon.className = `bi ${dark ? "bi-sun" : "bi-moon-stars"}`;
        themeToggle.setAttribute("aria-label", dark ? "Ativar modo claro" : "Ativar modo escuro");
        themeToggle.title = dark ? "Modo claro" : "Modo escuro";
        const themeColor = document.querySelector('meta[name="theme-color"]');
        if (themeColor) themeColor.setAttribute("content", dark ? "#100b09" : "#2a1c16");
    };

    const savedTheme = safeStorageGet("atelie-theme");
    if (savedTheme === "dark" || savedTheme === "light") root.dataset.theme = savedTheme;
    else if (window.matchMedia?.("(prefers-color-scheme: dark)").matches) root.dataset.theme = "dark";
    syncThemeIcon();

    themeToggle?.addEventListener("click", () => {
        root.dataset.theme = root.dataset.theme === "dark" ? "light" : "dark";
        safeStorageSet("atelie-theme", root.dataset.theme);
        syncThemeIcon();
    });

    // ---------------------------------------------------------------------
    // Voltar
    // ---------------------------------------------------------------------
    document.getElementById("globalBackButton")?.addEventListener("click", () => {
        if (document.referrer && new URL(document.referrer).origin === window.location.origin && window.history.length > 1)
            window.history.back();
        else
            window.location.assign("/");
    });

    // ---------------------------------------------------------------------
    // Menu mobile: Bootstrap é a única fonte de verdade.
    // ---------------------------------------------------------------------
    const collapseEl = document.getElementById("mainNavbar");
    const closeMobileMenu = () => {
        if (!collapseEl || window.innerWidth >= 992 || !window.bootstrap?.Collapse) return;
        window.bootstrap.Collapse.getOrCreateInstance(collapseEl).hide();
    };

    document.querySelectorAll("#mainNavbar a, #mainNavbar button[type=submit]").forEach(link => {
        link.addEventListener("click", closeMobileMenu);
    });

    document.addEventListener("keydown", event => {
        if (event.key === "Escape") {
            closeMobileMenu();
            closeNotifications();
        }
    });

    // ---------------------------------------------------------------------
    // Pesquisa
    // ---------------------------------------------------------------------
    const productSearch = document.getElementById("homeProductSearch");
    const productCards = [...document.querySelectorAll(".product-card-modern[data-product-title]")];
    const clientSearchEmpty = document.getElementById("clientSearchEmpty");
    const collectionMetaCount = document.querySelector(".collection-meta strong");
    const collectionMeta = document.querySelector(".collection-meta");

    if (productSearch && productCards.length) {
        const normalizeText = text => String(text ?? "")
            .toLocaleLowerCase("pt-BR")
            .normalize("NFD")
            .replace(/[\u0300-\u036f]/g, "");

        const filterProducts = () => {
            const term = normalizeText(productSearch.value.trim());
            let visibleCount = 0;

            productCards.forEach(card => {
                const title = normalizeText(card.dataset.productTitle);
                const matches = !term || title.includes(term);
                card.hidden = !matches;
                if (matches) visibleCount++;
            });

            if (collectionMetaCount) collectionMetaCount.textContent = visibleCount;
            if (collectionMeta) collectionMeta.hidden = visibleCount === 0;
            if (clientSearchEmpty) clientSearchEmpty.hidden = visibleCount !== 0 || !term;
        };

        productSearch.addEventListener("input", filterProducts);
        filterProducts();
    }

    // ---------------------------------------------------------------------
    // Animações com respeito a reduced motion.
    // ---------------------------------------------------------------------
    document.querySelectorAll(".product-card-modern, .auth-card, .feedback-order-card, .order-card").forEach(el => {
        if (!el.hasAttribute("data-animate")) el.dataset.animate = "fade";
    });

    const animatedElements = document.querySelectorAll("[data-animate]");
    if (window.matchMedia?.("(prefers-reduced-motion: reduce)").matches) {
        animatedElements.forEach(el => el.classList.add("is-animated"));
    } else if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (!entry.isIntersecting) return;
                entry.target.classList.add("is-animated");
                observer.unobserve(entry.target);
            });
        }, { threshold: 0.12, rootMargin: "0px 0px -4% 0px" });
        animatedElements.forEach(el => observer.observe(el));
    } else {
        animatedElements.forEach(el => el.classList.add("is-animated"));
    }

    // ---------------------------------------------------------------------
    // Notificações: SignalR em tempo real + fallback por sincronização HTTP.
    // ---------------------------------------------------------------------
    const notificationButton = document.getElementById("notificationButton");
    const notificationPanel = document.getElementById("notificationPanel");
    const notificationBadge = document.getElementById("notificationBadge");
    const notificationList = document.getElementById("notificationList");
    const clearNotifications = document.getElementById("clearNotifications");
    const markNotificationsRead = document.getElementById("markNotificationsRead");
    const toastRegion = document.getElementById("notificationToastRegion");

    const closeNotifications = () => {
        if (!notificationPanel || notificationPanel.hidden) return;
        notificationPanel.hidden = true;
        notificationButton?.setAttribute("aria-expanded", "false");
    };

    const statusIcon = status => ({
        0: "bi-file-earmark-plus", 1: "bi-hourglass-split", 2: "bi-check-circle",
        3: "bi-box-seam", 4: "bi-receipt", 5: "bi-truck", 6: "bi-house-check", 7: "bi-x-circle"
    }[Number(status)] || "bi-bell");

    const statusTone = status => ({
        6: "success",
        7: "danger",
        5: "info"
    }[Number(status)] || "neutral");

    if (notificationButton && notificationPanel && notificationList) {
        const userKey = document.body.dataset.userKey || "anonymous";
        const storageKey = `atelie-notifications:${userKey}`;
        const syncKey = `atelie-notifications-sync:${userKey}`;

        const loadNotifications = () => {
            try {
                const data = JSON.parse(localStorage.getItem(storageKey) || "[]");
                return Array.isArray(data) ? data.filter(x => x && typeof x === "object") : [];
            } catch { return []; }
        };

        let notifications = loadNotifications()
            .filter(item => item.createdAt && Date.now() - Number(item.createdAt) < 1000 * 60 * 60 * 24 * 30)
            .slice(0, 30);

        let lastSync = 0;
        try { lastSync = Number(localStorage.getItem(syncKey) || 0); } catch { lastSync = 0; }
        if (!Number.isFinite(lastSync) || lastSync <= 0) lastSync = Date.now() - 120000;

        const saveNotifications = () => safeStorageSet(storageKey, JSON.stringify(notifications.slice(0, 30)));
        const unreadCount = () => notifications.filter(item => !item.read).length;

        const showToast = item => {
            if (!toastRegion) return;
            const toast = document.createElement("div");
            toast.className = `notification-toast notification-toast-${statusTone(item.status)}`;
            toast.setAttribute("role", "status");
            toast.setAttribute("aria-live", "polite");

            const iconWrap = document.createElement("span");
            iconWrap.className = "notification-toast-icon";
            iconWrap.innerHTML = `<i class="bi ${statusIcon(item.status)}" aria-hidden="true"></i>`;

            const content = document.createElement("div");
            content.className = "notification-toast-content";
            const title = document.createElement("strong");
            title.textContent = item.title;
            const text = document.createElement("span");
            text.textContent = item.text;
            content.append(title, text);

            const close = document.createElement("button");
            close.type = "button";
            close.className = "notification-toast-close";
            close.setAttribute("aria-label", "Fechar notificação");
            close.innerHTML = '<i class="bi bi-x-lg" aria-hidden="true"></i>';
            close.addEventListener("click", event => {
                event.stopPropagation();
                toast.remove();
            });

            toast.append(iconWrap, content, close);
            toast.addEventListener("click", event => {
                if (event.target.closest("button")) return;
                window.location.assign(item.url);
            });
            toastRegion.appendChild(toast);

            requestAnimationFrame(() => toast.classList.add("is-visible"));
            window.setTimeout(() => {
                toast.classList.remove("is-visible");
                window.setTimeout(() => toast.remove(), 250);
            }, 7000);
        };

        const renderNotifications = () => {
            notificationList.replaceChildren();
            if (!notifications.length) {
                const empty = document.createElement("p");
                empty.className = "notification-empty";
                empty.innerHTML = '<i class="bi bi-bell-slash" aria-hidden="true"></i><span>Nenhuma notificação no momento.</span>';
                notificationList.appendChild(empty);
            } else {
                notifications.slice(0, 10).forEach(item => {
                    const row = document.createElement("a");
                    row.className = `notification-item${item.read ? " is-read" : ""}`;
                    row.href = item.url || "/Order";
                    row.dataset.notificationId = item.id;

                    const icon = document.createElement("span");
                    icon.className = "notification-item-icon";
                    icon.innerHTML = `<i class="bi ${statusIcon(item.status)}" aria-hidden="true"></i>`;
                    const content = document.createElement("span");
                    content.className = "notification-item-content";
                    const title = document.createElement("strong");
                    title.textContent = item.title || "Atualização do pedido";
                    const text = document.createElement("span");
                    text.textContent = item.text || "";
                    const time = document.createElement("small");
                    time.textContent = item.time || "Agora";
                    content.append(title, text, time);
                    row.append(icon, content);
                    row.addEventListener("click", () => {
                        const found = notifications.find(x => x.id === item.id);
                        if (found) { found.read = true; saveNotifications(); }
                    });
                    notificationList.appendChild(row);
                });
            }

            const unread = unreadCount();
            notificationBadge.hidden = unread === 0;
            notificationBadge.textContent = unread > 9 ? "9+" : String(unread);
            notificationButton.setAttribute("aria-label", unread
                ? `Notificações: ${unread} não lida${unread > 1 ? "s" : ""}`
                : "Abrir notificações");
        };

        const addNotification = (data, options = {}) => {
            if (!data?.orderId) return false;
            const status = Number(data.status);
            const statusName = data.statusName || "novo status";
            const orderNumber = data.orderNumber || `#${data.orderId}`;
            const delivered = status === 6;
            const cancelled = status === 7;
            const text = data.message || (delivered
                ? `O pedido ${orderNumber} foi entregue. Você já pode avaliar o produto.`
                : cancelled
                    ? `O pedido ${orderNumber} foi cancelado.`
                    : `O pedido ${orderNumber} foi atualizado para ${statusName}.`);

            const duplicate = notifications.some(item =>
                item.orderId === Number(data.orderId) &&
                item.status === status &&
                Math.abs(Number(item.createdAt || 0) - Number(data.updatedAtMs || Date.now())) < 120000);
            if (duplicate) return false;

            const item = {
                id: crypto.randomUUID?.() || `${Date.now()}-${Math.random()}`,
                orderId: Number(data.orderId),
                status,
                title: delivered ? "Pedido entregue" : cancelled ? "Pedido cancelado" : "Status do pedido atualizado",
                text,
                time: options.time || "Agora",
                url: delivered ? `/Order/History` : `/Order/Details/${encodeURIComponent(data.orderId)}`,
                read: false,
                createdAt: Number(data.updatedAtMs || Date.now())
            };

            notifications.unshift(item);
            notifications = notifications.slice(0, 30);
            saveNotifications();
            renderNotifications();
            if (options.toast !== false) showToast(item);
            window.dispatchEvent(new CustomEvent("order-status-updated", { detail: data }));
            return true;
        };

        const syncFromServer = async (showToastForFresh = true) => {
            if (document.body.dataset.authenticated !== "true") return;

            const sinceDate = new Date(Math.max(lastSync - 1500, Date.now() - 120000));
            try {
                const response = await fetch(`/Order/Notifications?since=${encodeURIComponent(sinceDate.toISOString())}`, {
                    cache: "no-store",
                    credentials: "same-origin",
                    headers: { "Accept": "application/json" }
                });
                if (!response.ok) return;
                const data = await response.json();
                let newest = lastSync;

                for (const item of Array.isArray(data) ? data : []) {
                    const timestamp = Date.parse(item.updatedAt || "") || Date.now();
                    newest = Math.max(newest, timestamp);
                    addNotification({
                        ...item,
                        updatedAtMs: timestamp,
                        message: item.status === 6
                            ? `O pedido ${item.orderNumber} foi entregue. Você já pode avaliar o produto.`
                            : item.status === 7
                                ? `O pedido ${item.orderNumber} foi cancelado.`
                                : `O pedido ${item.orderNumber} foi atualizado para ${item.statusName}.`
                    }, { toast: showToastForFresh && timestamp >= Date.now() - 120000 });
                }

                lastSync = Math.max(newest, Date.now() - 1000);
                safeStorageSet(syncKey, String(lastSync));
            } catch (_) {
                // SignalR continua sendo o canal principal; a próxima sincronização tenta novamente.
            }
        };

        const startSignalR = async () => {
            if (!window.signalR || document.body.dataset.authenticated !== "true") return;

            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/hubs/orders", { withCredentials: true })
                .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
                .configureLogging(signalR.LogLevel.Warning)
                .build();

            connection.on("StatusUpdated", data => {
                const timestamp = Date.parse(data?.updatedAt || "") || Date.now();
                addNotification({ ...data, updatedAtMs: timestamp }, { toast: true });
            });

            connection.on("AdminOrderStatusUpdated", data => {
                if (document.body.dataset.isAdmin !== "true") return;
                window.dispatchEvent(new CustomEvent("admin-order-status-updated", { detail: data }));
            });

            connection.onreconnecting(() => notificationButton.classList.add("is-connecting"));
            connection.onreconnected(() => {
                notificationButton.classList.remove("is-connecting");
                syncFromServer(true);
            });
            connection.onclose(() => notificationButton.classList.remove("is-connecting"));

            for (let attempt = 0; attempt < 5; attempt++) {
                try {
                    await connection.start();
                    notificationButton.classList.remove("is-connecting");
                    return;
                } catch (_) {
                    notificationButton.classList.add("is-connecting");
                    await new Promise(resolve => setTimeout(resolve, Math.min(1000 * 2 ** attempt, 10000)));
                }
            }
        };

        notificationButton.addEventListener("click", event => {
            event.stopPropagation();
            const opening = notificationPanel.hidden;
            notificationPanel.hidden = !opening;
            notificationButton.setAttribute("aria-expanded", String(opening));
        });

        markNotificationsRead?.addEventListener("click", () => {
            notifications.forEach(item => { item.read = true; });
            saveNotifications();
            renderNotifications();
        });

        clearNotifications?.addEventListener("click", () => {
            notifications = [];
            saveNotifications();
            renderNotifications();
        });

        document.addEventListener("click", event => {
            if (!notificationPanel.hidden && !notificationPanel.contains(event.target) && !notificationButton.contains(event.target))
                closeNotifications();
        });

        renderNotifications();
        syncFromServer(true);
        window.setInterval(() => syncFromServer(false), 8000);
        startSignalR();

        // Atualiza automaticamente a tela de histórico quando um pedido chega ao histórico.
        window.addEventListener("order-status-updated", event => {
            const status = Number(event.detail?.status);
            if (window.location.pathname.toLowerCase() === "/order/history" && [5, 6, 7].includes(status)) {
                window.setTimeout(() => window.location.reload(), 250);
            }
        });
    }

    // ---------------------------------------------------------------------
    // Canal administrativo em tempo real. O painel administrativo não usa
    // o sino do cliente, então mantém uma conexão dedicada e única.
    // ---------------------------------------------------------------------
    if (document.body.dataset.authenticated === "true" &&
        document.body.dataset.isAdmin === "true" &&
        window.signalR) {
        const adminConnection = new signalR.HubConnectionBuilder()
            .withUrl("/hubs/orders", { withCredentials: true })
            .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        adminConnection.on("AdminOrderStatusUpdated", data => {
            window.dispatchEvent(new CustomEvent("admin-order-status-updated", { detail: data }));
        });

        const startAdminSignalR = async () => {
            for (let attempt = 0; attempt < 5; attempt++) {
                try {
                    await adminConnection.start();
                    return;
                } catch (_) {
                    await new Promise(resolve => setTimeout(resolve, Math.min(1000 * 2 ** attempt, 10000)));
                }
            }
        };

        startAdminSignalR();
    }

    // ---------------------------------------------------------------------
    // Acessibilidade: links que abrem nova aba devem anunciar isso.
    // ---------------------------------------------------------------------
    document.querySelectorAll('a[target="_blank"]').forEach(link => {
        const rel = new Set((link.getAttribute("rel") || "").split(" ").filter(Boolean));
        rel.add("noopener");
        rel.add("noreferrer");
        link.setAttribute("rel", [...rel].join(" "));
    });
});
