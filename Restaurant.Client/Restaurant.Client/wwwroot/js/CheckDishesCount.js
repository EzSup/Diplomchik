function checkDishesCount() {
    const menuContainer = document.querySelector(".menu-container");
    const dishes = menuContainer.querySelectorAll(".dish-card"); // припускаємо, що ваш компонент має клас 'dish-card'

    if (dishes.length < 6) {
        console.log("6");
        menuContainer.classList.add('less-than-six');
        menuContainer.classList.remove('more-than-six');
    } else {
        console.log("no6");
        menuContainer.classList.add('more-than-six');
        menuContainer.classList.remove('less-than-six');
    }
};