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