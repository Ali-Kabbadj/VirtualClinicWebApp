$(() => {

	//On Scroll Functionality
	$(window).scroll(() => {
		var windowTop = $(window).scrollTop();
		windowTop > 100 ? $('nav').addClass('navShadow') : $('nav').removeClass('navShadow');
	});

	//Click Logo To Scroll To Top
	$('#brand').on('click', () => {
		$('html,body').animate({
			scrollTop: 0
		}, 500);
	});

	//Smooth Scrolling Using Navigation Menu
	$('a[href*="#"]').on('click', function (e) {
		$('html,body').animate({
			scrollTop: $($(this).attr('href')).offset().top - 100
		}, 500);
		e.preventDefault();
	});

	//Toggle Menu
	$('#menu-toggle').on('click', () => {
		$('#menu-toggle').toggleClass('closeMenu');
		$('ul').toggleClass('showMenu');

		$('li').on('click', () => {
			$('ul').removeClass('showMenu');
			$('#menu-toggle').removeClass('closeMenu');
		});
	});

});