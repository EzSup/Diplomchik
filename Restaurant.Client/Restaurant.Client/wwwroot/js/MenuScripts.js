function checkDishesCount() {
    const menuContainer = document.querySelector(".menu-container");
    const dishes = menuContainer.querySelectorAll(".dish-card"); // припускаємо, що ваш компонент має клас 'dish-card'

    if (dishes.length < 6) {
        menuContainer.classList.add('less-than-six');
        menuContainer.classList.remove('more-than-six');
    } else {
        menuContainer.classList.add('more-than-six');
        menuContainer.classList.remove('less-than-six');
    }
};

function StopPropagation(className) {
    const element = document.querySelector("." + className);
    if (element) {
        element.addEventListener("click", function (event) {
            event.stopPropagation();
        });
    }
}