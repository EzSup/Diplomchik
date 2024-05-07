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

function AddClassInView(element, className, threshold = 1, root = null) {
    let elements = document.querySelectorAll("."+ element);
    let observer = new IntersectionObserver(function (entries, observer) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add(className);
                observer.observe(entry.target);
            }
            //else {
            //    entry.target.classList.remove(className);
            //}
        });
    }, { threshold: threshold, root: root });

    elements.forEach(function (element) {
        observer.observe(element);
    });
}


function fadeInElement(elementId) {
    var element = document.getElementById(elementId);
    element.style.opacity = 0;
    element.style.transform = "translateY(-130px)";
    setTimeout(function () {
        element.style.transition = "opacity ease 0.3s, transform ease 1s";
        setTimeout(function () {
            fadeInElement(element);
        }, 1);
    }, 50);
    
    setTimeout(function () {
        var options = {
            threshold: 0.5
        };

        var observer = new IntersectionObserver(handleIntersection, options);
        observer.observe(element);
    }, 52);

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

