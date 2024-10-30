(function ($) {
	'use strict';

	$(function () {
		if( $('body').hasClass('motta-product-card-layout') ) {
			$('.motta-navigation-menu-element').find('.motta-navigation-menu__title').removeClass('motta-active');
			$('.motta-navigation-menu-element').find('.motta-navigation-menu').hide();
		}

		$('.motta-product-card-layout-2').find('#product-card-layout-2 .motta-navigation-menu__title').addClass('motta-active');
		$('.motta-product-card-layout-2').find('#product-card-layout-2 .motta-navigation-menu').show();

		$('.motta-product-card-layout-3').find('#product-card-layout-3 .motta-navigation-menu__title').addClass('motta-active');
		$('.motta-product-card-layout-3').find('#product-card-layout-3 .motta-navigation-menu').show();

		$('.motta-product-card-layout-4').find('#product-card-layout-4 .motta-navigation-menu__title').addClass('motta-active');
		$('.motta-product-card-layout-4').find('#product-card-layout-4 .motta-navigation-menu').show();

		$('.motta-product-card-layout-5').find('#product-card-layout-5 .motta-navigation-menu__title').addClass('motta-active');
		$('.motta-product-card-layout-5').find('#product-card-layout-5 .motta-navigation-menu').show();
	});

})(jQuery);
