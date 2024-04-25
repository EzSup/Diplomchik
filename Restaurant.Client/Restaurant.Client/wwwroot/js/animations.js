function animateElement(element, transition, timeout) {
    const myElement = document.getElementById(element);
    myElement.style.transition = `all ${transition}`;
    myElement.style.transform = `translateY(15%)`;
    myElement.style.opacity = '0';

    setTimeout(() => {
        myElement.style.transform = `translateY(0%)`;
        myElement.style.opacity = '1';
    }, timeout);
}

function fadeInElement(elementId) {
    // Функция для проверки, находится ли элемент в видимой области экрана
    function isElementVisible(el) {
        var rect = el.getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );
    }

    // Функция для анимации появления элемента
    function fadeInElement(el) {
        el.style.opacity = 0;
        el.style.transition = "opacity 1s ease, transform 1s ease";

        // Показываем элемент только если он в видимой области экрана
        if (isElementVisible(el)) {
            el.style.opacity = 1;
            el.style.transform = "translateY(0)";
        }

        // Проверяем видимость элемента при скроллинге
        window.addEventListener("scroll", function () {
            if (isElementVisible(el)) {
                el.style.opacity = 1;
                el.style.transform = "translateY(0)";
            }
        });
    }

    // Вызываем функцию для элемента с указанным идентификатором
    document.addEventListener("DOMContentLoaded", function () {
        var element = document.getElementById(elementId);
        if (element) {
            fadeInElement(element);
        }
    });
};