
let startX = 0;
let endX = 0;
let currentSlideIndex = 0;

function showSlide(index) {
	const slides = document.querySelectorAll(".slide");
	const ellipses = document.querySelectorAll(".ellipse");
	const currentSlide = (index + slides.length) % slides.length;
	const slideWidth = slides[0].getBoundingClientRect().width;

	slides.forEach(function (slide) {
		slide.classList.remove("active");
		slide.classList.remove("prev");
		slide.classList.remove("next");
	});

	ellipses.forEach(function (ellipse) {
		ellipse.classList.remove("active");
		ellipse.style.transition = "all 1s ease";
	});

	slides.forEach(function (slide, i) {
		const distance = Math.min(Math.abs(i - currentSlide), Math.abs(slides.length + i - currentSlide), Math.abs(i - slides.length - currentSlide));
		slide.style.zIndex = slides.length - distance;
		const leftPosition = (i - currentSlide) * slideWidth;
		slide.style.left = (i - currentSlide) * (slideWidth + 100) + "px";
	});

	slides[currentSlide].classList.add("active");

	slides[(currentSlide + 1) % slides.length].classList.add("next");
	slides[(currentSlide - 1 + slides.length) % slides.length].classList.add("prev");

	ellipses[currentSlide].classList.add("active");

	currentSlideIndex = currentSlide;
}

function handleTouchStart(event) {
	startX = event.touches[0].clientX;
	console.log(startX);
}

function handleTouchMove(event) {
	endX = event.touches[0].clientX;
	console.log(endX);
}

function handleTouchEnd() {
	if (startX - endX > 50) {
		showSlide(currentSlideIndex + 1);
	} else if (startX - endX < -50) {
		showSlide(currentSlideIndex - 1);
	}
}

document.addEventListener('DOMContentLoaded', (event) => {
	const slider = document.querySelector('.slide');
	if (slider) {
		slider.addEventListener('touchstart', (event) => handleTouchStart(event));
		slider.addEventListener('touchmove', (event) => handleTouchMove(event));
		slider.addEventListener('touchend', (event) => handleTouchEnd(event));
	}

	// Initial call to display the first slide correctly
	showSlide(currentSlideIndex);
});