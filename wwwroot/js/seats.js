(function () {
    const map = document.getElementById('seat-map');
    if (!map) return;

    const screeningId = map.dataset.screeningId;
    const ticketPrice = parseFloat(map.dataset.ticketPrice || '10');
    const selected = new Map();
    const summaryEl = document.getElementById('selected-summary');
    const totalEl = document.getElementById('selected-total');
    const bookBtn = document.getElementById('book-btn');
    const nameInput = document.getElementById('customer-name');
    const scaleEl = document.getElementById('seat-map-scale');
    let scale = 1;

    function seatKey(row, seat) {
        return row + '-' + seat;
    }

    function updateSummary() {
        if (selected.size === 0) {
            summaryEl.textContent = 'Места не выбраны';
            totalEl.textContent = '0 ₼';
            bookBtn.disabled = true;
            return;
        }

        const labels = Array.from(selected.values())
            .sort((a, b) => a.row - b.row || a.seat - b.seat)
            .map(s => 'Ряд ' + s.row + ', м.' + s.seat);

        summaryEl.textContent = labels.join('; ');
        totalEl.textContent = (selected.size * ticketPrice).toFixed(0) + ' ₼';
        bookBtn.disabled = false;
    }

    map.addEventListener('click', function (e) {
        const btn = e.target.closest('.seat-available, .seat-selected');
        if (!btn || btn.disabled) return;

        const row = parseInt(btn.dataset.row, 10);
        const seat = parseInt(btn.dataset.seat, 10);
        const key = seatKey(row, seat);

        if (selected.has(key)) {
            selected.delete(key);
            btn.classList.remove('seat-selected');
            btn.classList.add('seat-available');
        } else {
            selected.set(key, { row: row, seat: seat });
            btn.classList.remove('seat-available');
            btn.classList.add('seat-selected');
        }

        updateSummary();
    });

    document.getElementById('zoom-in')?.addEventListener('click', function () {
        scale = Math.min(scale + 0.1, 1.5);
        scaleEl.style.transform = 'scale(' + scale + ')';
    });

    document.getElementById('zoom-out')?.addEventListener('click', function () {
        scale = Math.max(scale - 0.1, 0.7);
        scaleEl.style.transform = 'scale(' + scale + ')';
    });

    bookBtn?.addEventListener('click', async function () {
        const name = nameInput?.value?.trim();
        if (!name) {
            alert('Введите ваше имя');
            nameInput?.focus();
            return;
        }

        if (selected.size === 0) {
            alert('Выберите места');
            return;
        }

        const seats = Array.from(selected.values());
        const formData = new FormData();
        formData.append('customerName', name);
        formData.append('seatsJson', JSON.stringify(seats));

        bookBtn.disabled = true;
        bookBtn.innerHTML = '<span class="spinner-border spinner-border-sm"></span> Оформление...';

        try {
            const response = await fetch('/screening/' + screeningId + '/book', {
                method: 'POST',
                body: formData
            });

            const result = await response.json();

            if (result.success) {
                window.location.href = '/booking/' + result.bookingId + '/ticket';
            } else {
                alert(result.message || 'Не удалось оформить заказ');
                bookBtn.disabled = false;
                bookBtn.innerHTML = '<i class="bi bi-ticket-perforated me-1"></i> Забронировать';
            }
        } catch {
            alert('Ошибка сети. Попробуйте снова.');
            bookBtn.disabled = false;
            bookBtn.innerHTML = '<i class="bi bi-ticket-perforated me-1"></i> Забронировать';
        }
    });
})();
