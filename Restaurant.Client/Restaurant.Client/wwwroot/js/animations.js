async function animateElement(element, transition, timeout) {
    const myElement = document.getElementById(element);
    myElement.style.transition = `all ${transition}`;
    myElement.style.transform = `translateY(15%)`;
    myElement.style.opacity = '0';

    setTimeout(() => {
        myElement.style.transform = `translateY(0%)`;
        myElement.style.opacity = '1';
    }, timeout);
}

async function AddClassInView(element, className, threshold = 1, bool = false, root = null) {
    let elements = document.querySelectorAll("." + element);
    let observer = new IntersectionObserver(function (entries, observer) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add(className);
            } else {
            if (bool) {
                entry.target.classList.remove(className);
            }
            }
        });
    }, { threshold: threshold, root: root });

    elements.forEach(function (element) {
        observer.observe(element);
    });
}




async function fadeInElement(elementId, translateYValue) {
    var element = document.getElementById(elementId);
    element.style.opacity = 0;
    element.style.transform = "translateY(-130px)";
    setTimeout(async function () {
        element.style.transition = "opacity ease 0.3s, transform ease 1s";
        setTimeout(async function () {
            fadeInElement(element);
        }, 1);
    }, 50);
    
    setTimeout(async function () {
        var options = {
            threshold: 0.3
        };

        var observer = new IntersectionObserver(handleIntersection, options);
        observer.observe(element);
    }, 52);

    async function fadeInElement(el) {
            el.style.opacity = 1;
            el.style.transform = "translateY(0)";
    }

    async function handleIntersection(entries) {
        entries.forEach(async function (entry) {
            if (entry.isIntersecting) {
                element.style.opacity = 1;
                element.style.transform = "translateY(0)";
            } else {
                element.style.opacity = 0;
                element.style.transform = "translateY(" + translateYValue + ")";
            }
        });
    }
};


//function AnimateMenuButtons() {
//    const objects = document.querySelectorAll(".filter input, .filter label", ".filter label");
//        const objectsArray = Array.from(objects);
//        const shuffledObjects = objectsArray.sort(() => Math.random() - 0.5);

//        setTimeout(() => {
//            shuffledObjects.forEach((obj, index) => {
//                setTimeout(() => {
//                    obj.style.opacity = 1;
//                }, Math.pow((index * 100), 0.8));
//            });
//        }, 1000);
//}

async function AnimateMenuButtons() {
    const observer = new MutationObserver((mutationsList, observer) => {
        const objects = document.querySelectorAll(".filter input, .filter label, .scrollbar label");
        if (objects.length > 0) {
            observer.disconnect(); 
            const objectsArray = Array.from(objects);
            const shuffledObjects = objectsArray.sort(() => Math.random() - 0.5);

            setTimeout(() => {
                shuffledObjects.forEach((obj, index) => {
                    setTimeout(() => {
                        obj.style.opacity = 1;
                    }, Math.pow((index * 100), 0.8));
                });
            }, 1000);
        }
    });
    observer.observe(document.body, { childList: true, subtree: true });
}

function shuffleShefs(containerID, shefsClass) {
    const container = document.querySelector("." + containerID);
    const shefs = Array.from(document.querySelectorAll("." + shefsClass));

    for (let i = shefs.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [shefs[i], shefs[j]] = [shefs[j], shefs[i]];
    }

    shefs.forEach((shef, index) => {
        container.appendChild(shef);
        const randomMarginTop = Math.floor(Math.random() * 30) * 0.1 + 0.5;
        const randomMarginRight = Math.floor(Math.random() * 30) * 0.1 + 0.5;
        const randomMarginBottom = Math.floor(Math.random() * 30) * 0.1 + 0.5;
        const randomMarginLeft = Math.floor(Math.random() * 30) * 0.1 + 0.5;
        const delay = (Math.random() * 0.5).toFixed(1);

        shef.style.marginTop = `${randomMarginTop}%`;
        shef.style.marginRight = `${randomMarginRight}%`;
        shef.style.marginBottom = `${randomMarginBottom}%`;
        shef.style.marginLeft = `${randomMarginLeft}%`;
        shef.style.transitionDelay = `${delay}s`;
    });
}
