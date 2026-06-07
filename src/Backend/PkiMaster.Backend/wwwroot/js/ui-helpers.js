export async function openOffcanvasAsync(element) {
    return new Promise((resolve) => {
        try {
            if (!element) return resolve(false);
            const bsInstance = bootstrap.Offcanvas.getInstance(element) || new bootstrap.Offcanvas(element);
            const onShown = () => {
                element.removeEventListener('shown.bs.offcanvas', onShown);
                resolve(true);
            };
            element.addEventListener('shown.bs.offcanvas', onShown);
            bsInstance.show();
            // Fallback in case event isn't fired (timeout slightly longer than animation)
            setTimeout(() => {
                element.removeEventListener('shown.bs.offcanvas', onShown);
                resolve(true);
            }, 700);
        } catch (e) {
            resolve(false);
        }
    });
}

export async function closeOffcanvasAsync(element) {
    return new Promise((resolve) => {
        try {
            if (!element) return resolve(false);
            const bsInstance = bootstrap.Offcanvas.getInstance(element) || new bootstrap.Offcanvas(element);
            const onHidden = () => {
                element.removeEventListener('hidden.bs.offcanvas', onHidden);
                resolve(true);
            };
            element.addEventListener('hidden.bs.offcanvas', onHidden);
            bsInstance.hide();
            // Fallback in case event isn't fired
            setTimeout(() => {
                element.removeEventListener('hidden.bs.offcanvas', onHidden);
                resolve(true);
            }, 700);
        } catch (e) {
            resolve(false);
        }
    });
}