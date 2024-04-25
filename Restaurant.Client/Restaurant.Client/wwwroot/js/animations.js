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
    var element = document.getElementById(elementId);
    element.style.opacity = 0;
    element.style.transform = "translateY(-130px)";
    setTimeout(function () {
        element.style.transition = "opacity ease 0.3s, transform ease 1s";
        setTimeout(function () {
            fadeInElement(element);
        }, 250);
    }, 250);
    

    
    setTimeout(function () {
        var options = {
            threshold: 0.5
        };

        var observer = new IntersectionObserver(handleIntersection, options);
        observer.observe(element);
    }, 500);

    function fadeInElement(el) {
            el.style.opacity = 1;
            el.style.transform = "translateY(0)";
    }

    function handleIntersection(entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                element.style.opacity = 1;
                element.style.transform = "translateY(0)";
            } else {
                element.style.opacity = 0;
                element.style.transform = "translateY(-50px)";
            }
        });
    }
};