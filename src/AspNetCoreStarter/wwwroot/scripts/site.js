/* Site-specific JavaScript */


// Add red asterix to label of required fields

document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('input, select').forEach(field => {
        var req = field.hasAttribute('data-val-required') && !field.classList.contains("no-asterix");
        if (req != undefined && req) {
            var label = document.querySelectorAll('label[for="' + field.getAttribute('id') + '"]').item(0);
            if (label != undefined) {
                var text = label.innerHTML;
                if (text.length > 0) {
                    label.insertAdjacentHTML("beforeend", '<span class="has-text-danger"> *</span>');
                }
            }
        }
    });
});