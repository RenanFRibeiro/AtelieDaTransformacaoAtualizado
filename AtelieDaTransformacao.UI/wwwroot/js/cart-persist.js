// Persistência simples do carrinho no localStorage e importação automática ao entrar (login)
(function () {
    const CART_KEY = 'atelier_cart';
    const badge = document.getElementById('cartCountBadge');

    function getLocalCart() {
        try {
            return JSON.parse(localStorage.getItem(CART_KEY)) || [];
        } catch (e) {
            return [];
        }
    }

    function setLocalCart(items) {
        localStorage.setItem(CART_KEY, JSON.stringify(items));
        updateBadge();
    }

    function updateBadge() {
        if (!badge) return;
        if (typeof atelierIsAuthenticated !== 'undefined' && atelierIsAuthenticated) {
            // buscar do servidor
            fetch('/Cart/Count').then(r => r.json()).then(n => {
                if (n > 0) {
                    badge.style.display = 'inline-block';
                    badge.textContent = n;
                } else badge.style.display = 'none';
            }).catch(() => badge.style.display = 'none');
        } else {
            const items = getLocalCart();
            const count = items.reduce((s, i) => s + (i.quantity || i.Quantity || 0), 0);
            if (count > 0) {
                badge.style.display = 'inline-block';
                badge.textContent = count;
            } else badge.style.display = 'none';
        }
    }

    // Ao detectar usuário autenticado, envia o carrinho local para o servidor e limpa o localStorage
    if (typeof atelierIsAuthenticated !== 'undefined' && atelierIsAuthenticated) {
        const local = getLocalCart();
        if (local.length > 0) {
            fetch('/Cart/Import', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(local)
            }).then(() => {
                localStorage.removeItem(CART_KEY);
                updateBadge();
            }).catch(() => updateBadge());
        } else {
            updateBadge();
        }
    } else {
        updateBadge();
    }

    // Expor uma função global para que páginas de produto possam adicionar ao cart local sem precisar de autenticação.
    window.atelierAddToLocalCart = function (product) {
        // product = { productId, quantity, title, image, price }
        const cart = getLocalCart();
        const existing = cart.find(i => i.productId === product.productId);
        if (existing) {
            existing.quantity = (existing.quantity || 0) + (product.quantity || 1);
        } else {
            cart.push({
                productId: product.productId,
                quantity: product.quantity || 1,
                title: product.title || '',
                image: product.image || '',
                price: product.price || 0
            });
        }
        setLocalCart(cart);
    };
})();