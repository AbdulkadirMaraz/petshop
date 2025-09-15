(function ($) {
    "use strict";
	
	var $window = $(window); 
	var $body = $('body'); 

	/* Preloader Effect JS */
	$window.on("load", () => $(".preloader").fadeOut(600));
	
	/* Sticky Header JS */	
	if($('.active-sticky-header').length){
		
		$window.on('resize', function(){
			setHeaderHeight();
		});

		function setHeaderHeight(){
	 		$("header.main-header").css("height", $('header .header-sticky').outerHeight());
		}	
	
		$(window).on("scroll", function() {
			var fromTop = $(window).scrollTop();
			setHeaderHeight();
			var headerHeight = $('header .header-sticky').outerHeight()
			$("header .header-sticky").toggleClass("hide", (fromTop > headerHeight + 100));
			$("header .header-sticky").toggleClass("active", (fromTop > 600));
		});
	}

	/* Mobile Menu Handling */
	const initialMenuItems = $('#menu > li').toArray();
	const initialMenu2Items = $('#menu2 > li').toArray();

	const handleMobileMenus = () => {
        const isMobile = $window.width() <= 768;
        const hasSlickNav = $(".slicknav_nav").length > 0;

        if (isMobile && !hasSlickNav) {
            $("#menu2").children().appendTo("#menu");
            $("#menu").slicknav({ label: "", prependTo: ".responsive-menu" });
        } else if (!isMobile && hasSlickNav) {
            $("#menu").slicknav("destroy");

            $("#menu > li").not(initialMenuItems).appendTo("#menu2");
            initialMenu2Items.forEach((item) => $(item).appendTo("#menu2"));
            initialMenuItems.forEach((item) => $(item).appendTo("#menu"));
        }
    };

	/* Run the function on page load */
    handleMobileMenus();
	
	let resizeTimeout;

	/* Re-run the function on window resize */
	$window.on("resize", function () {
		clearTimeout(resizeTimeout);
		resizeTimeout = setTimeout(handleMobileMenus, 200); // Delay execution
	});
	
	/* Scroll to Top */
    $(document).on("click", "a[href='#top']", function (e) {
        e.preventDefault();
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });

	/* Initialize Swiper Sliders */
    const initSwiper = (selector, options) => {
        if ($(selector).length) {
            return new Swiper(selector, options);
        }
        return null;
    };

	const swiperOptions = {
        slidesPerView: 1,
        speed: 1000,
        spaceBetween: 10,
        loop: true,
        autoplay: { delay: 5000 },
    };

	initSwiper(".hero-slider-layout .swiper", {
        ...swiperOptions,
        autoplay: { delay: 4000 },
        pagination: { el: ".hero-pagination", clickable: true },
        navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
    });

	initSwiper(".sisf-testimonioal-slider .swiper", {
        ...swiperOptions,
        navigation: { nextEl: ".testimonioal-next", prevEl: ".testimonioal-prev" },
    });

	initSwiper(".service-single-slider .swiper", {
        ...swiperOptions,
        navigation: { nextEl: ".service-single-button-next", prevEl: ".service-single-button-prev" },
    });

	/* Skill Bar */
	if ($('.skills-progress-bar').length) {
		$('.skills-progress-bar').waypoint(function() {
			$('.skillbar').each(function() {
				$(this).find('.count-bar').animate({
				width:$(this).attr('data-percent')
				},2000);
			});
		},{
			offset: '50%'
		});
	}

	/* Youtube Background Video JS */
	if ($('#herovideo').length) {
		var myPlayer = $("#herovideo").YTPlayer();
	}

	/* Audio JS */
	const player = new Plyr('#player');

	/* Init Counter */
	if ($('.counter').length) {
		$('.counter').counterUp({ delay: 6, time: 3000 });
	}

	/* Image Reveal Animation */
	if ($('.reveal').length) {
        gsap.registerPlugin(ScrollTrigger);
        let revealContainers = document.querySelectorAll(".reveal");
        revealContainers.forEach((container) => {
            let image = container.querySelector("img");
            let tl = gsap.timeline({
                scrollTrigger: {
                    trigger: container,
                    toggleActions: "play none none none"
                }
            });
            tl.set(container, {
                autoAlpha: 1
            });
            tl.from(container, 1, {
                xPercent: -100,
                ease: Power2.out
            });
            tl.from(image, 1, {
                xPercent: 100,
                scale: 1,
                delay: -1,
                ease: Power2.out
            });
        });
    }

	/* Text Effect Animation */
	if ($('.text-anime-style-1').length) {
		let staggerAmount 	= 0.05,
			translateXValue = 0,
			delayValue 		= 0.5,
		   animatedTextElements = document.querySelectorAll('.text-anime-style-1');
		
		animatedTextElements.forEach((element) => {
			let animationSplitText = new SplitText(element, { type: "chars, words" });
				gsap.from(animationSplitText.words, {
				duration: 1,
				delay: delayValue,
				x: 20,
				autoAlpha: 0,
				stagger: staggerAmount,
				scrollTrigger: { trigger: element, start: "top 85%" },
				});
		});		
	}
	
	if ($('.text-anime-style-2').length) {				
		let	 staggerAmount 		= 0.03,
			 translateXValue	= 20,
			 delayValue 		= 0.1,
			 easeType 			= "power2.out",
			 animatedTextElements = document.querySelectorAll('.text-anime-style-2');
		
		animatedTextElements.forEach((element) => {
			let animationSplitText = new SplitText(element, { type: "chars, words" });
				gsap.from(animationSplitText.chars, {
					duration: 1,
					delay: delayValue,
					x: translateXValue,
					autoAlpha: 0,
					stagger: staggerAmount,
					ease: easeType,
					scrollTrigger: { trigger: element, start: "top 85%"},
				});
		});		
	}
	
	if ($('.text-anime-style-3').length) {		
		let	animatedTextElements = document.querySelectorAll('.text-anime-style-3');
		
		 animatedTextElements.forEach((element) => {
			//Reset if needed
			if (element.animation) {
				element.animation.progress(1).kill();
				element.split.revert();
			}

			element.split = new SplitText(element, {
				type: "lines,words,chars",
				linesClass: "split-line",
			});
			gsap.set(element, { perspective: 400 });

			gsap.set(element.split.chars, {
				opacity: 0,
				x: "50",
			});

			element.animation = gsap.to(element.split.chars, {
				scrollTrigger: { trigger: element,	start: "top 90%" },
				x: "0",
				y: "0",
				rotateX: "0",
				opacity: 1,
				duration: 1,
				ease: Back.easeOut,
				stagger: 0.02,
			});
		});		
	}

	/* Parallaxie JS */
	var $parallaxie = $('.parallaxie');
	if($parallaxie.length && ($window.width() > 991))
	{
		if ($window.width() > 768) {
			$parallaxie.parallaxie({
				speed: 0.55,
				offset: 0,
			});
		}
	}

	/* Zoom Gallery Screenshot JS */
	$('.gallery-items').magnificPopup({
		delegate: 'a',
		type: 'image',
		closeOnContentClick: false,
		closeBtnInside: false,
		mainClass: 'mfp-with-zoom',
		image: {
			verticalFit: true,
		},
		gallery: {
			enabled: true
		},
		zoom: {
			enabled: true,
			duration: 300, // don't foget to change the duration also in CSS
			opener: function(element) {
			  return element.find('img');
			}
		}
	});

	/* Contact Form Validation JS */
	$("#contactForm").validator({ focus: false }).on("submit", function (event) {
        if (!event.isDefaultPrevented()) {
            event.preventDefault();
            submitForm("#contactForm", "../form-process.php", contactFormSuccess);
        }
    });

	const submitForm = (formId, url, successCallback) => {
        const formData = $(formId).serialize();
        $.post(url, formData, (response) => {
			if (typeof response === "string" && response.trim() === "success") {
				successCallback();
			} else {
				showMsg(false, response);
			}
		});
    };

	const contactFormSuccess = () => {
        $("#contactForm")[0].reset();
        showMsg(true, "Message Sent Successfully!");
    };

    const showMsg = (valid, msg) => {
        $("#msgSubmit").removeClass().addClass(valid ? "text-success" : "text-danger").text(msg);
    };
	/* End - Contact Form Validation JS */

	/* Animated Wow Js */	
	new WOW().init();

	/* Popup Video JS */
	if ($('.popup-video').length) {
		$('.popup-video').magnificPopup({
			type: 'iframe',
			mainClass: 'mfp-fade',
			removalDelay: 160,
			preloader: false,
			fixedContentPos: true
		});
	}

	/* Gallery Slider Image Carousel JS */
	if ($('.gallery-slide').length) {
		initSwiper(".gallery-slide .swiper", {
			...swiperOptions,
			slidesPerView : 3,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 3 },
				1920: { slidesPerView: 3 }
			}
		});
	}

	/* Gallery Sliders Image Carousel JS */
	if ($('.gallery-slides').length) {
		initSwiper(".gallery-slides .swiper", {
			...swiperOptions,
			slidesPerView : 4,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 4 },
				1920: { slidesPerView: 3 }
			}
		});
	}

	/* Image Slider Carousel JS */
	if ($('.sisf-sis-image-slider').length) {
		initSwiper(".sisf-sis-image-slider .swiper", {
			...swiperOptions,
			slidesPerView: "auto",
			centeredSlides: true,  
			autoplay: {
				delay: 5000,
				disableOnInteraction: false, 
			},
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 5 },
				1920: { slidesPerView: 3 },
			}
		});
	}

	/* Product List More to Love Slider JS */
	if ($('.product-list-more-slider').length) {
		initSwiper(".product-list-more-slider .swiper", {
			...swiperOptions,
			slidesPerView : 3,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 3 },
				1920: { slidesPerView: 3 }
			}
		});
	}
	
	/* Product List Slider JS */
	if ($('.product-list-slider').length) {
		initSwiper(".product-list-slider .swiper", {
			...swiperOptions,
			slidesPerView: "5",
			centeredSlides: true,  
			loopedSlides: 5, 
			autoplay: {
				delay: 5000,
				disableOnInteraction: false, 
			},
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 5 },
				1920: { slidesPerView: 3 },
			}
		});
	}	
	
	/* Product List Slider Inner JS */
	if ($('.product-list-slider-inner').length) {
		initSwiper(".product-list-slider-inner .swiper", {
			...swiperOptions,
			slidesPerView : 3,
			centeredSlides: true,  
			loopedSlides: 5, 
			autoplay: {
				delay: 5000,
				disableOnInteraction: false, 
			},
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 3 },
				1920: { slidesPerView: 3 },
			}
		});
	}

	/* Category Slider JS */
	if ($('.sisf-sis-category-slider').length) {
		initSwiper(".sisf-sis-category-slider .swiper", {
			...swiperOptions,
			slidesPerView : 5,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 5 },
				1920: { slidesPerView: 5 },
			}
		});
	}

	/* Blog Slider JS */
	if ($('.sisf-blog-slider').length) {
		initSwiper(".sisf-blog-slider .swiper", {
			...swiperOptions,
			slidesPerView : 3,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 4 },
				1920: { slidesPerView: 3 },
			}
		});
	}

	/* Testimonials Slider JS */
	if ($('.sisf-testimonials-slider').length) {
		initSwiper(".sisf-testimonials-slider .swiper", {
			...swiperOptions,
			slidesPerView : 1,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
		});
	}

	/* Partner Logo Slider JS */
	if ($('.partener-logo-slider').length) {
		initSwiper(".partener-logo-slider .swiper", {
			...swiperOptions,
			slidesPerView : 7,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 7 },
				1920: { slidesPerView: 3 },
			}
		});
	}

	/* Brand Logo Slider JS */
	if ($('.brand-logo-slider').length) {
		initSwiper(".brand-logo-slider .swiper", {
			...swiperOptions,
			slidesPerView : 6,
			navigation: { nextEl: ".core-value-button-next", prevEl: ".core-value-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 2 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 6 },
				1920: { slidesPerView: 3 },
			}
		});
	}

	/* Full Width Image Slider JS */
	if ($('.full-width-image-slider').length) {
		initSwiper(".full-width-image-slider .swiper", {
			...swiperOptions,
			slidesPerView : 1,
			navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 1 },
				767: { slidesPerView: 1 },
				1024: { slidesPerView: 1 },
				1367: { slidesPerView: 1 },
				1920: { slidesPerView: 1 },
			}
		});
	}

	/* Product Details Slider JS */
	if ($('.product-detail-slider').length) {
		initSwiper(".product-detail-slider .swiper", {
			...swiperOptions,
			slidesPerView : 1,
			navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 1 },
				767: { slidesPerView: 1 },
				1024: { slidesPerView: 1 },
				1367: { slidesPerView: 1 },
				1920: { slidesPerView: 1 },
			}
		});
	}

	/* Full Width Slider JS */
	if ($('.full-width-slider').length) {
		initSwiper(".full-width-slider .swiper", {
			...swiperOptions,
			slidesPerView : 4,
			navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 1 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 4 },
				1920: { slidesPerView: 4 },
			}
		});
	}

	/* Inner Pages Slider JS */
	if ($('.inner-pages-slider').length) {
		initSwiper(".inner-pages-slider .swiper", {
			...swiperOptions,
			slidesPerView : 4,
			navigation: { nextEl: ".swiper-button-next", prevEl: ".swiper-button-prev" },
			breakpoints: {
				320: { slidesPerView: 1 },
				480: { slidesPerView: 1 },
				650: { slidesPerView: 1 },
				767: { slidesPerView: 2 },
				1024: { slidesPerView: 2 },
				1367: { slidesPerView: 4 },
				1920: { slidesPerView: 4 },
			}
		});
	}

	/* Shop Detail Page Tabs Icon JS */
	document.querySelectorAll('.product-tab-nav .nav-link').forEach(button => {
		button.addEventListener('click', () => {
		  	document.querySelectorAll('.product-tab-nav .nav-link .icon').forEach(icon => {
				icon.classList.replace('fa-minus', 'fa-plus');
		  	});
	  		const icon = button.querySelector('.icon');
		  	if (icon) {
				icon.classList.replace('fa-plus', 'fa-minus');
		  	}
		});
	});

	$(function () {

		// Shop List With Filter Category Button
		const $hiddenCategoryItems = $('.hidden-item');
		$hiddenCategoryItems.hide();
		
		$('.toggle-icon').removeClass('fa-minus').addClass('fa-plus');
		$('.toggle-text').text('View More');
		
		$(document).on('click', '#toggleView', function () {
			const isVisible = $hiddenCategoryItems.first().is(':visible');
			$hiddenCategoryItems.toggle(!isVisible);
		
			$('.toggle-icon')
			.toggleClass('fa-plus', isVisible)
			.toggleClass('fa-minus', !isVisible);
			$('.toggle-text').text(isVisible ? 'View More' : 'View Less');
		});
		
		// Shop List With Filter Brand Button
		const $hiddenBrandItems = $('.hidden-brand');
		$hiddenBrandItems.hide();
		
		$('.toggle-icon-brand').removeClass('fa-minus').addClass('fa-plus');
		$('.toggle-text-brand').text('View More');
	
		$(document).on('click', '#toggleViewBrand', function () {
			const isVisible = $hiddenBrandItems.first().is(':visible');
			$hiddenBrandItems.toggle(!isVisible);
		
			$('.toggle-icon-brand')
			.toggleClass('fa-plus', isVisible)
			.toggleClass('fa-minus', !isVisible);
			$('.toggle-text-brand').text(isVisible ? 'View More' : 'View Less');
		});
	});
	
	/* Woocommerce Product Quantity Plus Minus JS */
	$(document).on("click", ".sisf-quantity-minus, .sisf-quantity-plus", function (e) {
        e.preventDefault();
        const $button = $(this);
        const $inputField = $button.siblings(".sisf-quantity-input");
        const step = parseFloat($inputField.data("step")) || 1;
        const max = parseFloat($inputField.data("max"));
        const min = parseFloat($inputField.data("min")) || 1;
        let inputValue = parseFloat($inputField.val()) || min;

        inputValue = $button.hasClass("sisf-quantity-minus") ? Math.max(min, inputValue - step) : (Number.isNaN(max) ? inputValue + step : Math.min(max, inputValue + step));

        $inputField.val(inputValue).trigger("change");
    });		

})(jQuery);