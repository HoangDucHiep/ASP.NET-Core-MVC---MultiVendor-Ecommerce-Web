class MottaNavigationMenuHandler extends elementorModules.frontend.handlers.Base {
	bindEvents() {
		const 	settings 	= this.getElementSettings();

		var $title = this.$element.find('.motta-navigation-menu__title');

		jQuery( window ).on('resize', function () {
			if ( jQuery( window ).width() > 1024 && settings.toggle_menu == "yes") {


				if ( settings.toggle_status == "yes" ) {
					$title.removeClass( 'motta-active' ).addClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).css( 'display', 'block' );
				} else {
					$title.removeClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).removeAttr('style');
				}

				$title.addClass( 'motta-navigation-menu__title--toggle' );
				$title.next( '.motta-navigation-menu' ).addClass( 'clicked' );
				$title.closest( '.motta-navigation-menu-element' ).addClass( 'dropdown' );

			} else if ( jQuery( window ).width() < 1025 && jQuery( window ).width() > 767 && settings.toggle_menu_tablet == "yes") {
				$title.addClass( 'motta-navigation-menu__title--toggle' );
				$title.next( '.motta-navigation-menu' ).addClass( 'clicked' );
				$title.closest( '.motta-navigation-menu-element' ).addClass( 'dropdown' );

				if ( settings.toggle_status_tablet == "yes" ) {
					$title.addClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).css( 'display', 'block' );
				} else {
					$title.removeClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).removeAttr('style');
				}

			} else if ( jQuery( window ).width() < 768 && settings.toggle_menu_mobile == "yes") {
				$title.addClass( 'motta-navigation-menu__title--toggle' );
				$title.next( '.motta-navigation-menu' ).addClass( 'clicked' );
				$title.closest( '.motta-navigation-menu-element' ).addClass( 'dropdown' );

				if ( settings.toggle_status_mobile == "yes" ) {
					$title.addClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).css( 'display', 'block' );
				} else {
					$title.removeClass( 'motta-active' );
					$title.siblings( '.motta-navigation-menu' ).removeAttr('style');
				}

			} else {
				$title.removeClass( 'motta-navigation-menu__title--toggle' );
				$title.removeClass( 'motta-active' );
				$title.siblings( '.motta-navigation-menu' ).removeAttr('style');
				$title.next('.motta-navigation-menu').removeClass('clicked');
				$title.next('.motta-navigation-menu').removeAttr('style');
				$title.closest('.motta-navigation-menu-element').removeClass('dropdown');
			}
		}).trigger('resize');

		this.$element.find( '.motta-navigation-menu__title' ).append( '<span class="motta-svg-icon navigation-menu__plus"><svg viewBox="0 0 32 32"><path d="M26.667 13.333h-8v-8h-5.333v8h-8v5.333h8v8h5.333v-8h8z"></path></svg></span>' );
		this.$element.find( '.motta-navigation-menu__title' ).append( '<span class="motta-svg-icon navigation-menu__minus"><svg viewBox="0 0 32 32"><path d="M26.667 13.333v5.333h-21.333v-5.333h21.333z"></path></svg></span>' );

		this.$element.on( 'click', '.motta-navigation-menu__title--toggle', function ( e ) {
			e.preventDefault();

			if ( !$title.closest( '.motta-navigation-menu-element' ).hasClass( 'dropdown' ) ) {
				return;
			}

			jQuery(this).next('.clicked').stop().slideToggle();
			jQuery(this).toggleClass('motta-active');
			return false;
		} );
	}
}

class MottaNavigationBarWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-navigation-bar'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	bindEvents() {
		jQuery( '.motta-navigation-bar__title' ).on( 'click', function() {
			jQuery( '.motta-navigation-bar__title' ).removeClass( 'active' );
			jQuery( this ).addClass( 'active' );
		});
	}

	stickyNavigationBar() {
		var navbar = this.elements.$container,
			wrapper = navbar.parent().parent().parent(),
			sticky = navbar.offset().top ;

		jQuery(window).on( 'scroll', function () {

			if ( window.pageYOffset <= wrapper.outerHeight() ) {
				if ( window.pageYOffset >= sticky ) {
					navbar.addClass( "sticky-navigation-bar" )

					var headerSticky = jQuery( '.motta-header-sticky .header-sticky' ).outerHeight() + 32;
					navbar.css( 'top', headerSticky );
				} else {
					navbar.removeClass( "sticky-navigation-bar" );
					navbar.removeAttr('style');
				};

			} else {
				navbar.removeClass( "sticky-navigation-bar" );
				navbar.removeAttr('style');
				return;
			}
        });
	}

	onInit() {
		super.onInit();
		this.stickyNavigationBar();
	}
}

class MottaCountDownWidgetHandler extends elementorModules.frontend.handlers.Base {

	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-countdown'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	getCountDownInit() {
		this.elements.$container.motta_countdown();
	}

	onInit() {
		super.onInit();
		this.getCountDownInit();
	}
}

class MottaTeamMemberGridWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				grid: '.motta-team-member-grid__wrapper',
				filter: '.motta-team-member-grid__tags',
			}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );
		return {
			$grid: this.findElement( selectors.grid ),
			$filter: this.findElement( selectors.filter ),
		};
	}

	bindEvents(){
		jQuery( '.motta-team-member-grid__tags-item' ).on( 'click', function() {
			var $this = jQuery( this ),
				tags = $this.data( 'tag' ),
				wrapper = $this.parent().siblings( '.motta-team-member-grid__wrapper' );

			$this.parent().find( '.motta-team-member-grid__tags-item' ).removeClass( 'active' );
			$this.addClass( 'active' );

			if( tags ) {
				wrapper.find( '.motta-team-member-grid__item' ).not( tags ).removeClass( 'fadeIn' ).addClass( 'fadeOut' );
				wrapper.find( '.motta-team-member-grid__item' + tags ).removeClass( 'fadeIn fadeOut' ).addClass( 'fadeIn' );
			}

			if( wrapper.find( '.motta-team-member-grid__item' ).hasClass( 'fadeIn' ) ){
				setTimeout( function(){
					wrapper.find( '.motta-team-member-grid__item' + tags ).removeClass( 'fadeIn' );
				}, 310 )
			}
		});
	}

	onInit() {
		super.onInit();
	}
}

class MottaAccordionWidgetHandler extends elementorModules.frontend.handlers.Base {

	getDefaultSettings() {
		return {
			selectors: {
				tab: '.motta-accordion__title',
				panel: '.motta-accordion__content'
			},
			classes: {
				active: 'motta-tab--active',
			},
			showFn: 'slideDown',
			hideFn: 'slideUp',
			autoExpand: false,
			toggleSelf: true,
			hidePrevious: true
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$tabs: this.findElement( selectors.tab ),
			$panels: this.findElement( selectors.panel )
		};
	}

	activateDefaultTab() {
		const settings = this.getSettings();

		if ( ! settings.autoExpand || 'editor' === settings.autoExpand && ! this.isEdit ) {
			return;
		}

		const defaultActiveTab = this.getEditSettings( 'activeItemIndex' ) || 1,
			originalToggleMethods = {
				showFn: settings.showFn,
				hideFn: settings.hideFn
			};

		this.setSettings( {
			showFn: 'show',
			hideFn: 'hide'
		} );

		this.changeActiveTab( defaultActiveTab );

		this.setSettings( originalToggleMethods );
	}

	changeActiveTab( tabIndex ) {
		const settings = this.getSettings(),
			$tab = this.elements.$tabs.filter( '[data-tab="' + tabIndex + '"]' ),
			$panel = this.elements.$panels.filter( '[data-tab="' + tabIndex + '"]' ),
			isActive = $tab.hasClass( settings.classes.active );

		if ( ! settings.toggleSelf && isActive ) {
			return;
		}

		if ( ( settings.toggleSelf || ! isActive ) && settings.hidePrevious ) {
			this.elements.$tabs.removeClass( settings.classes.active );
			this.elements.$tabs.parent().removeClass( settings.classes.active );
			this.elements.$panels.removeClass( settings.classes.active )[settings.hideFn]();
		}

		if ( ! settings.hidePrevious && isActive ) {
			$tab.removeClass( settings.classes.active );
			$tab.parent().removeClass( settings.classes.active );
			$panel.removeClass( settings.classes.active )[settings.hideFn]();
		}

		if ( ! isActive ) {
			$tab.addClass( settings.classes.active );
			$tab.parent().addClass( settings.classes.active );
			$panel.addClass( settings.classes.active )[settings.showFn]();
		}
	}

	bindEvents() {
		this.elements.$tabs.on( {
			keydown: ( event ) => {
				if ( 'Enter' !== event.key ) {
					return;
				}

				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-tab' ) );
			},
			click: ( event ) => {
				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-tab' ) );
			}
		} );
	}

	onInit() {
		super.onInit();
		this.activateDefaultTab();
	}
}

class MottaSlidesWidgetHandler extends elementorModules.frontend.handlers.SwiperBase {
	getDefaultSettings() {
		return {
			selectors: {
				carousel: '.swiper-container',
				slideContent: '.swiper-slide',
			},
		}
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		const elements = {
			$swiperContainer: this.$element.find( selectors.carousel ),
		};

		elements.$slides = elements.$swiperContainer.find( selectors.slideContent );

		return elements;
	}

	getCarouselOptions() {
		const elementSettings = this.getElementSettings(),
				self 		= this,
		 		container 	= self.elements.$swiperContainer,
				slidesToShow = elementSettings.slides_to_show || 3,
				slidesToScroll = elementSettings.slides_to_scroll_mobile || elementSettings.slides_to_scroll_tablet || elementSettings.slides_to_scroll || 1,
				isSingleSlide = 1 === slidesToShow,
				defaultLGDevicesSlidesCount = isSingleSlide ? 1 : 2,
				swiperOptions = this.getSettings( 'swiperOptions' ),
				elementorBreakpoints = elementorFrontend.config.responsive.activeBreakpoints;

		var centeredSlides = elementSettings.centeredSlides == 'yes' ? true : false;

		let carouselSettings = {
			slidesPerView: slidesToShow,
			loop: centeredSlides == true ? true : 'yes' === elementSettings.infinite,
			speed: elementSettings.autoplay_speed,
			handleElementorBreakpoints: true,
			watchOverflow: true,
			watchSlidesProgress: true,
			effect: centeredSlides == true || elementSettings.slides_to_show > 1 ? 'slide' : elementSettings.effect,
			fadeEffect: {
				crossFade: true
			},
			centeredSlides: centeredSlides,
			on              : {
				init: function() {
					container.closest('.motta-slides-elementor').css( 'opacity', 1 );
					var index         = this.activeIndex,
					currentSlide     = jQuery(this.slides[index]),
					$data_arrow 	= currentSlide.data( 'arrow' ),
					currentSlideType = currentSlide.closest(".motta-slides-elementor");

					if( $data_arrow && $data_arrow.color ) {
						currentSlideType.find( '.motta-swiper-button' ).css( 'color', $data_arrow.color );
					} else {
						currentSlideType.find( '.motta-swiper-button' ).css( 'color', '' );
					}

					if( $data_arrow && $data_arrow.background_color ) {
						currentSlideType.find( '.motta-swiper-button' ).css( 'background-color', $data_arrow.background_color );
					} else {
						currentSlideType.find( '.motta-swiper-button' ).css( 'background-color', '' );
					}
				},

				paginationUpdate: function() {
					var index         = this.activeIndex,
						currentSlide     = jQuery(this.slides[index]),
						$data_dots 		= currentSlide.data( 'dots' ),
						currentSlideType = currentSlide.closest(".motta-slides-elementor");

					//dot
					if( $data_dots && $data_dots.color ) {
						currentSlideType.find( '.swiper-pagination-bullet' ).css( 'background-color', $data_dots.color );
					} else {
						currentSlideType.find( '.swiper-pagination-bullet' ).css( 'background-color', '' );
					}

					if( $data_dots && $data_dots.color_active ) {
						currentSlideType.find( '.swiper-pagination-bullet-active, .swiper-pagination-bullet:hover' ).css( 'background-color', $data_dots.color_active );
					} else {
						currentSlideType.find( '.swiper-pagination-bullet-active, .swiper-pagination-bullet:hover' ).css( 'background-color', '' );
					}

				},

				slideChange: function() {
					var index         = this.activeIndex,
						currentSlide     = jQuery(this.slides[index]),
						$data_dots 		= currentSlide.data( 'dots' ),
						$data_arrow 	= currentSlide.data( 'arrow' ),
						currentSlideType = currentSlide.closest(".motta-slides-elementor");

					//dot
					if( $data_dots.color ) {
						currentSlideType.find( '.swiper-pagination-bullet' ).css( 'background-color', $data_dots.color );
					} else {
						currentSlideType.find( '.swiper-pagination-bullet' ).css( 'background-color', '' );
					}

					if( $data_dots.color_active ) {
						currentSlideType.find( '.swiper-pagination-bullet-active, .swiper-pagination-bullet:hover' ).css( 'background-color', $data_dots.color_active );
					} else {
						currentSlideType.find( '.swiper-pagination-bullet-active, .swiper-pagination-bullet:hover' ).css( 'background-color', '' );
					}

					//arrow
					if( $data_arrow.color ) {
						currentSlideType.find( '.motta-swiper-button' ).css( 'color', $data_arrow.color );
					} else {
						currentSlideType.find( '.motta-swiper-button' ).css( 'color', '' );
					}

					if( $data_arrow.background_color ) {
						currentSlideType.find( '.motta-swiper-button' ).css( 'background-color', $data_arrow.background_color );
					} else {
						currentSlideType.find( '.motta-swiper-button' ).css( 'background-color', '' );
					}
				},
			},
			breakpoints: {},
		};

		var mtspaceBetween, mtspaceBetweenTablet, mtspaceBetweenMobile = 0;
		if ( elementSettings.space_between ) {
			var mtspacedefault = elementSettings.centeredSlides == true ? 8 : 24,
				mtspaceBetween = elementSettings.space_between.size ? elementSettings.space_between.size : mtspacedefault;
			carouselSettings.spaceBetween = mtspaceBetween;
		}

		if ( elementSettings.space_between_tablet ) {
			var mtspaceBetweenTablet = elementSettings.space_between_tablet.size ? elementSettings.space_between_tablet.size : mtspaceBetween;
		}

		if ( elementSettings.space_between_mobile ) {
			var mtspaceBetweenMobile = elementSettings.space_between_mobile.size ? elementSettings.space_between_mobile.size : mtspaceBetween;
		}

		carouselSettings.breakpoints[ elementorBreakpoints.mobile.value ] = {
			slidesPerView: elementSettings.slides_to_show_mobile ? elementSettings.slides_to_show_mobile : 1,
			slidesPerGroup: elementSettings.slides_to_scroll_mobile ? elementSettings.slides_to_scroll_mobile : 1,
			spaceBetween: mtspaceBetweenMobile
		};

		carouselSettings.breakpoints[ elementorBreakpoints.tablet.value ] = {
			slidesPerView: elementSettings.slides_to_show_tablet || defaultLGDevicesSlidesCount,
			slidesPerGroup: elementSettings.slides_to_scroll_tablet || 1,
			spaceBetween: mtspaceBetweenTablet
		};

		if ( 'yes' === elementSettings.autoplay ) {
			carouselSettings.autoplay = {
				delay: elementSettings.autoplay_speed
			};
		}

		if ( ! isSingleSlide ) {
			carouselSettings.slidesPerGroup = slidesToScroll;
		}

		carouselSettings.navigation = {
			prevEl: this.$element.find( '.motta-swiper-button-prev' ).get(0),
			nextEl: this.$element.find( '.motta-swiper-button-next' ).get(0),
		};

		carouselSettings.pagination = {
			el: this.$element.find( '.swiper-pagination' ).get(0),
			type: 'bullets',
			clickable: true,
		};

		if ( swiperOptions ) {
			carouselSettings = _.extend( swiperOptions, carouselSettings );
		}

		return carouselSettings;
	}

	async onInit( ...args ) {
		super.onInit( ...args );

		if ( ! this.elements.$swiperContainer.length || 1 > this.elements.$slides.length ) {
			return;
		}

		const Swiper = elementorFrontend.utils.swiper;

		this.swiper = await new Swiper( this.elements.$swiperContainer, this.getCarouselOptions( true ) );

		this.elements.$swiperContainer.data( 'swiper', this.swiper );

		if ( 'yes' === this.getElementSettings( 'pause_on_hover' ) ) {
			this.togglePauseOnHover( true );
		}
	}

	onElementChange( propertyName ) {
		switch ( propertyName ) {
			case 'pause_on_hover':
				const pauseable = this.getElementSettings( 'pause_on_hover' );

				this.togglePauseOnHover( 'yes' === pauseable );
				break;

			case 'autoplay_speed':
				this.swiper.params.autoplay.delay = this.getElementSettings( 'autoplay_speed' );
				this.swiper.update();
				break;

			case 'speed':
				this.swiper.params.speed = this.getElementSettings( 'speed' );
				this.swiper.update();
				break;
		}
	}

	onEditSettingsChange( propertyName ) {
		if ( 'activeItemIndex' === propertyName ) {
			this.swiper.slideToLoop( this.getEditSettings( 'activeItemIndex' ) - 1 );
		}
	}
}

class MottaBannerImageWidgetHandler extends elementorModules.frontend.handlers.Base {

	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-banner'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	getCountDownInit() {
		if (typeof motta_countdown !== 'undefined') {
			this.elements.$container.find('.motta-countdown').motta_countdown();
		}
	}

	onInit() {
		super.onInit();
		this.getCountDownInit();
	}
}

class MottaCountDownBannerWidgetHandler extends elementorModules.frontend.handlers.Base {

	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-countdown-banner'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	getCountDownInit() {
		if (typeof motta_countdown !== 'undefined') {
			this.elements.$container.find('.motta-countdown').motta_countdown();
		}
	}

	onInit() {
		super.onInit();
		this.getCountDownInit();
	}
}

class MottaProductsTabsHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				tab: '.motta-tabs__nav li',
				panel: '.motta-tabs__panel',
				products: 'ul.products',
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$tabs: this.findElement( selectors.tab ),
			$panels: this.findElement( selectors.panel ),
			$products: this.findElement( selectors.products ),
		};
	}

	activateDefaultTab() {
		const defaultActiveTab = this.getEditSettings( 'activeItemIndex' ) || 1;

		if ( this.isEdit ) {
			jQuery( document.body ).trigger( 'motta_products_loaded', [this.elements.$products.find( 'li.product' ), false] );
		}

		this.changeActiveTab( defaultActiveTab );
	}

	changeActiveTab( tabIndex ) {
		if ( this.isActiveTab( tabIndex ) ) {
			return;
		}

		const $tab = this.getTab( tabIndex ),
			$panel = this.getPanel( tabIndex );

		$tab.addClass( 'active' ).siblings( '.active' ).removeClass( 'active' );

		if ( $panel.length ) {
			$panel.addClass( 'active' ).siblings( '.active' ).removeClass( 'active' );
		} else {
			this.loadNewPanel( tabIndex );
		}
	}

	isActiveTab( tabIndex ) {
		return this.getTab( tabIndex ).hasClass( 'active' );
	}

	hasTabPanel( tabIndex ) {
		return this.getPanel( tabIndex ).length;
	}

	getTab( tabIndex ) {
		return this.elements.$tabs.filter( '[data-target="' + tabIndex + '"]' );
	}

	getPanel( tabIndex ) {
		return this.elements.$panels.filter( '[data-panel="' + tabIndex + '"]' );
	}

	getProductCarousel($selector, settings) {
        $selector.find('.woocommerce').addClass('swiper-container');
        $selector.find('ul.products').addClass('swiper-wrapper');
        $selector.find('li.product').addClass('swiper-slide');
        $selector.find('.woocommerce').after('<span class="motta-svg-icon motta-svg-icon--left motta-swiper-button-prev swiper-button motta-swiper-button"><svg width="24" height="24" aria-hidden="true" role="img" focusable="false" viewBox="0 0 32 32"><path d="M20.58 2.58l2.84 2.84-10.6 10.58 10.6 10.58-2.84 2.84-13.4-13.42z"></path></svg></span>');
        $selector.find('.woocommerce').after('<span class="motta-svg-icon motta-svg-icon--right motta-swiper-button-next swiper-button motta-swiper-button"><svg width="24" height="24" aria-hidden="true" role="img" focusable="false" viewBox="0 0 32 32"><path d="M11.42 29.42l-2.84-2.84 10.6-10.58-10.6-10.58 2.84-2.84 13.4 13.42z"></path></svg></span>');
        $selector.find('.woocommerce').after('<div class="swiper-pagination"></div>');

        var options = {
            loop: settings.infinite == 'yes' ? true : false,
            autoplay: settings.autoplay == 'yes' ? true : false,
            speed: settings.speed ? settings.speed : 500,
            watchOverflow: true,
            pagination: {
                el: $selector.find('.swiper-pagination').get(0),
                clickable: true
            },
			navigation: {
                nextEl: $selector.find('.motta-swiper-button-next').get(0),
                prevEl: $selector.find('.motta-swiper-button-prev').get(0),
            },
            on: {
                init: function () {
                    this.$el.css('opacity', 1);
                }
            },
            breakpoints: {
                0: {
                    slidesPerView: settings.slides_to_show_mobile ? settings.slides_to_show_mobile : mottaData.mobile_product_columns,
                    slidesPerGroup: settings.slides_to_scroll_mobile ? settings.slides_to_scroll_mobile : mottaData.mobile_product_columns,
                },
                768: {
                    slidesPerView: settings.slides_to_show_tablet ? settings.slides_to_show_tablet : 3,
                    slidesPerGroup: settings.slides_to_scroll_tablet ? settings.slides_to_scroll_tablet : 1,
                },
                1024: {
                    slidesPerView: settings.slides_to_show ? settings.slides_to_show : 5,
                    slidesPerGroup: settings.slides_to_scroll ? settings.slides_to_scroll : 1,
                },
            }
        };

        new Swiper($selector.find('.woocommerce'), options);
    };

	loadNewPanel( tabIndex ) {
		if ( this.hasTabPanel( tabIndex ) ) {
			return;
		}

		const isEdit = this.isEdit,
			  $tab = this.elements.$tabs.filter( '[data-target="' + tabIndex + '"]' ),
			  $panelsContainer = this.elements.$panels.first().parent(),
			  atts = $tab.data( 'atts' ),
			  $settings = this.getElementSettings(),
			  ajax_url = wc_add_to_cart_params ? wc_add_to_cart_params.wc_ajax_url.toString().replace(  '%%endpoint%%', 'motta_get_products_tab' ) : mottaData.ajax_url;

		if ( ! atts ) {
			return;
		}

		$panelsContainer.addClass( 'loading' );

		jQuery.post( ajax_url, {
			action: 'motta_get_products_tab',
			atts  : atts,
			settings: $settings
		}, ( response ) => {
			if ( !response.success ) {
				$panelsContainer.removeClass( 'loading' );
				return;
			}

			const $newPanel = this.elements.$panels.first().clone(),
				  $settings = this.getElementSettings();

			$newPanel.html( response.data );
			$newPanel.attr( 'data-panel', tabIndex );
			$newPanel.addClass( 'active' );
			$newPanel.appendTo( $panelsContainer );
			$newPanel.siblings( '.active' ).removeClass( 'active' );

			if ( $panelsContainer.closest('.motta-product-tabs').hasClass('motta-product-tabs-carousel--elementor') ) {
				this.getProductCarousel($newPanel, $settings);
			}

			if ( ! $settings.pagination_enable ) {
				$newPanel.find('.woocommerce-navigation__products-tabs').remove();
			}

			this.elements.$panels = this.elements.$panels.add( $newPanel );

			if ( ! isEdit ) {
				jQuery( document.body ).trigger( 'motta_products_loaded', [$newPanel.find( 'li.product' ), false] );
			}

			setTimeout( () => {
				$panelsContainer.removeClass( 'loading' );
			}, 500 );
		} );
	}

	loadMoreProducts() {
		var ajax_url = mottaData.ajax_url.toString().replace('%%endpoint%%', 'motta_elementor_load_products' );

		// Load Products
		this.$element.on( 'click', '.woocommerce-navigation__products-tabs a', function (e) {
			e.preventDefault();

			var $el = jQuery(this),
				$els = jQuery(this).closest( '.woocommerce-navigation__products-tabs' ),
				$panel = $el.closest( '.motta-tabs__panel' ).attr( 'data-panel' ),
				$settings = $el.closest( '.motta-product-tabs--elementor' ).find( "li[data-target='" + $panel + "']" ).data( 'atts' );

				if ( $els.hasClass('loading')) {
				return;
			}

			$els.addClass( 'loading' );

			jQuery.post(
				ajax_url,
				{
					page: $el.attr( 'data-page' ),
					settings: $settings
				},
				function ( response ) {
					if ( ! response ) {
						return;
					}

					$els.removeClass( 'loading' );

					var $data = jQuery( response.data ),
						$products = $data.find( 'li.product' ),
						$container = $el.closest( '.motta-tabs__panel' ),
						$grid = $container.find( 'ul.products' ),
						$page_number = $data.find( '.page-number' ).data( 'page' );

					if ( $products.length ) {
						$products.addClass( 'animated mottaFadeInUp' );

						$grid.append($products);

						if ($page_number == '0') {
							$el.closest( '.woocommerce-navigation__products-tabs' ).remove();
						} else {
							$el.attr( 'data-page', $page_number );
						}
					}

					jQuery(document.body).trigger( 'motta_products_loaded', [ $products, true ] );
				}
			);
		});
	};

	bindEvents() {
		this.elements.$tabs.on( {
			click: ( event ) => {
				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-target' ) );
			}
		} );
	}

	onInit( ...args ) {
		super.onInit( ...args );

		if ( this.elements.$panels.closest('.motta-product-tabs').hasClass('motta-product-tabs-carousel--elementor') ) {
			const settings = this.getElementSettings();

			this.getProductCarousel(this.elements.$panels, settings);
		}

		this.activateDefaultTab();

		this.loadMoreProducts();
	}
}

class MottaSwiperCarouselWidgetHandler extends elementorModules.frontend.handlers.SwiperBase {
	getDefaultSettings() {
		return {
			selectors: {
				carousel: '.swiper-container',
				slideContent: '.swiper-slide',
			},
		}
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		const elements = {
			$swiperContainer: this.$element.find( selectors.carousel ),
		};

		elements.$slides = elements.$swiperContainer.find( selectors.slideContent );

		return elements;
	}

	getCarouselOptions() {
		const elementSettings = this.getElementSettings(),
				slidesToShow = elementSettings.slides_to_show || 3,
				slidesToScroll = elementSettings.slides_to_scroll_mobile || elementSettings.slides_to_scroll_tablet || elementSettings.slides_to_scroll || 1,
				isSingleSlide = 1 === slidesToShow,
				defaultLGDevicesSlidesCount = isSingleSlide ? 1 : 2,
				swiperOptions = this.getSettings( 'swiperOptions' ),
				elementorBreakpoints = elementorFrontend.config.responsive.activeBreakpoints;

		let carouselSettings = {
			slidesPerView: slidesToShow,
			loop: 'yes' === elementSettings.infinite,
			speed: elementSettings.speed,
			handleElementorBreakpoints: true,
			watchOverflow: true,
			watchSlidesProgress: true,
			observer: true,
			observeParents: true,
			breakpoints: {},
		};

		var mtspaceBetweenTablet = 0,
			mtspaceBetweenMobile = 0;

		if ( elementSettings.space_between ) {
			var mtspaceBetween = elementSettings.space_between.size ? elementSettings.space_between.size : 24;
			carouselSettings.spaceBetween = mtspaceBetween;
		}

		if ( elementSettings.space_between_tablet ) {
			mtspaceBetweenTablet = elementSettings.space_between_tablet.size ? elementSettings.space_between_tablet.size : mtspaceBetween;
		}

		if ( elementSettings.space_between_mobile ) {
			mtspaceBetweenMobile = elementSettings.space_between_mobile.size ? elementSettings.space_between_mobile.size : mtspaceBetween;
		}

		carouselSettings.breakpoints[ elementorBreakpoints.mobile.value ] = {
			slidesPerView: elementSettings.slides_to_show_mobile && elementSettings.slides_to_show_mobile !== 'undefined' ? elementSettings.slides_to_show_mobile : parseInt(mottaData.mobile_product_columns),
			slidesPerGroup: elementSettings.slides_to_scroll_mobile && elementSettings.slides_to_scroll_mobile !== 'undefined' ? elementSettings.slides_to_scroll_mobile : parseInt(mottaData.mobile_product_columns),
			spaceBetween: mtspaceBetweenMobile
		};

		carouselSettings.breakpoints[ elementorBreakpoints.tablet.value ] = {
			slidesPerView: elementSettings.slides_to_show_tablet || defaultLGDevicesSlidesCount,
			slidesPerGroup: elementSettings.slides_to_scroll_tablet || 1,
			spaceBetween: mtspaceBetweenTablet
		};

		if ( 'yes' === elementSettings.autoplay ) {
			carouselSettings.autoplay = {
				delay: elementSettings.autoplay_speed
			};
		}

		if ( ! isSingleSlide ) {
			carouselSettings.slidesPerGroup = slidesToScroll;
		}

		carouselSettings.navigation = {
			prevEl: this.$element.find( '.motta-swiper-button-prev' ).get(0),
			nextEl: this.$element.find( '.motta-swiper-button-next' ).get(0),
		};

		carouselSettings.pagination = {
			el: this.$element.find( '.swiper-pagination' ).get(0),
			type: 'bullets',
			clickable: true,
		};

		if ( elementSettings.navigation == 'scrollbar' || elementSettings.navigation_tablet == 'scrollbar' || elementSettings.navigation_mobile == 'scrollbar' || elementSettings.navigation == 'arrows-scrollbar' || elementSettings.navigation_tablet == 'arrows-scrollbar' || elementSettings.navigation_mobile == 'arrows-scrollbar' ) {
			carouselSettings.scrollbar = {
				el: this.$element.find( '.swiper-scrollbar' ).get(0),
				hide: false,
				draggable: true,
			};
		}

		if ( swiperOptions ) {
			carouselSettings = _.extend( swiperOptions, carouselSettings );
		}

		if ( carouselSettings.slidesPerView === 'auto' ) {
			let slidesPerViewTablet = elementSettings.slides_to_show_tablet ? elementSettings.slides_to_show_tablet : 'auto';
			slidesPerViewTablet = 'auto' === slidesPerViewTablet ? slidesPerViewTablet : +slidesPerViewTablet;

			let slidesPerViewMobile = elementSettings.slides_to_show_mobile ? elementSettings.slides_to_show_mobile : slidesPerViewTablet;
			slidesPerViewMobile = 'auto' === slidesPerViewMobile ? slidesPerViewMobile : +slidesPerViewMobile;

			carouselSettings.breakpoints[ elementorBreakpoints.tablet.value ].slidesPerView = slidesPerViewTablet;
			carouselSettings.breakpoints[ elementorBreakpoints.mobile.value ].slidesPerView = slidesPerViewMobile;
		}

		if ( 'products' === this.getSettings( 'carouselContent' ) ) {
			const carouselEvents = {};

			if ( this.isEdit ) {
				carouselEvents.beforeInit = () => {
					jQuery( document.body ).trigger( 'motta_products_loaded', [this.elements.$slides, false] );
				};
			}

			if ( carouselEvents ) {
				carouselSettings.on = carouselEvents;
			}
		}

		return carouselSettings;
	}

	getCountDownInit() {
		this.elements.$swiperContainer.closest('.motta-product-carousel').find('.motta-countdown').motta_countdown();
		this.elements.$swiperContainer.closest('.motta-product-deals').find('.motta-countdown').motta_countdown();
	}

	async onInit( ...args ) {
		super.onInit( ...args );

		await this.getCountDownInit();

		if ( ! this.elements.$swiperContainer.length || 1 > this.elements.$slides.length ) {
			return;
		}

		var $wrapper = this.$element.find('.motta-product-carousel');

		if ( $wrapper.find('ul.products').hasClass('product-card-layout-5') || $wrapper.find('ul.products').hasClass('product-card-layout-3') ) {
			$wrapper.addClass('motta-carousel-spacing-empty');
		}

		const Swiper = elementorFrontend.utils.swiper,
			  elementSettings = this.getElementSettings();

		if ( elementSettings.navigation == 'scrollbar' || elementSettings.navigation_tablet == 'scrollbar' || elementSettings.navigation_mobile == 'scrollbar' || elementSettings.navigation == 'arrows-scrollbar' || elementSettings.navigation_tablet == 'arrows-scrollbar' || elementSettings.navigation_mobile == 'arrows-scrollbar' ) {
			if( $wrapper.find( '.motta-product-carousel__container').length == 1 ) {
				$wrapper.find( '.motta-product-carousel__container').append('<div class="swiper-scrollbar"></div>');
			} else {
				$wrapper.append('<div class="swiper-scrollbar"></div>');
			}
		}

		this.elements.$swiperContainer.find('ul.products').addClass( "swiper-wrapper" );

		if ( this.elements.$swiperContainer.hasClass('motta-gallery-carousel__list') ) {
			this.elements.$swiperContainer.find('div.gallery').addClass( "swiper-wrapper" );
			this.elements.$swiperContainer.find('figure.gallery-item').addClass( "swiper-slide" );
		}

		this.swiper = await new Swiper( this.elements.$swiperContainer, this.getCarouselOptions( true ) );

		this.elements.$swiperContainer.data( 'swiper', this.swiper );

		if ( 'yes' === this.getElementSettings( 'pause_on_hover' ) ) {
			this.togglePauseOnHover( true );
		}
	}

	onElementChange( propertyName ) {
		switch ( propertyName ) {
			case 'pause_on_hover':
				const pauseable = this.getElementSettings( 'pause_on_hover' );

				this.togglePauseOnHover( 'yes' === pauseable );
				break;

			case 'autoplay_speed':
				this.swiper.params.autoplay.delay = this.getElementSettings( 'autoplay_speed' );
				this.swiper.update();
				break;

			case 'speed':
				this.swiper.params.speed = this.getElementSettings( 'speed' );
				this.swiper.update();
				break;
		}
	}

	onEditSettingsChange( propertyName ) {
		if ( 'activeItemIndex' === propertyName ) {
			this.swiper.slideToLoop( this.getEditSettings( 'activeItemIndex' ) - 1 );
		}
	}
}

class MottaInstagramCarouselWidgetHandler extends elementorModules.frontend.handlers.SwiperBase {
	getDefaultSettings() {
		return {
			selectors: {
				carousel: '.swiper-container',
				slideContent: '.swiper-slide',
			},
		}
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		const elements = {
			$swiperContainer: this.$element.find( selectors.carousel ),
		};

		elements.$slides = elements.$swiperContainer.find( selectors.slideContent );

		return elements;
	}

	getCarouselOptions() {
		const elementSettings = this.getElementSettings(),
				slidesToShow = elementSettings.slides_to_show || 3,
				slidesToScroll = elementSettings.slides_to_scroll_mobile || elementSettings.slides_to_scroll_tablet || elementSettings.slides_to_scroll || 1,
				slidesToRows = elementSettings.slides_to_rows_mobile || elementSettings.slides_to_rows_tablet || elementSettings.slides_to_rows || 1,
				isSingleSlide = 1 === slidesToShow,
				swiperOptions = this.getSettings( 'swiperOptions' );

		let carouselSettings = {
			slidesPerView: slidesToShow,
			slidesPerColumn: slidesToRows,
			loop: 'yes' === elementSettings.infinite,
			speed: elementSettings.speed,
			watchOverflow: true,
			watchSlidesProgress: true,
			observer: true,
			observeParents: true,
			breakpoints: {
				0: {
                    slidesPerView: elementSettings.slides_to_show_mobile ? elementSettings.slides_to_show_mobile : slidesToShow,
                    slidesPerGroup: elementSettings.slides_to_scroll_mobile ? elementSettings.slides_to_scroll_mobile : slidesToScroll,
                    slidesPerColumn: elementSettings.slides_to_rows_mobile ? elementSettings.slides_to_rows_mobile : slidesToRows,
                },
				768: {
                    slidesPerView: elementSettings.slides_to_show_tablet || slidesToShow,
                    slidesPerGroup: elementSettings.slides_to_scroll_tablet || 1,
                    slidesPerColumn: elementSettings.slides_to_rows_tablet ? elementSettings.slides_to_rows_tablet : slidesToRows,
                },
                1024: {
                    slidesPerView: slidesToShow,
                    slidesPerGroup: slidesToScroll,
                    slidesPerColumn: slidesToRows,
                },
			},
		};

		if ( 'yes' === elementSettings.autoplay ) {
			carouselSettings.autoplay = {
				delay: elementSettings.autoplay_speed
			};
		}

		if ( ! isSingleSlide ) {
			carouselSettings.slidesPerGroup = slidesToScroll;
		}

		carouselSettings.navigation = {
			prevEl: this.$element.find( '.motta-swiper-button-prev' ).get(0),
			nextEl: this.$element.find( '.motta-swiper-button-next' ).get(0),
		};

		carouselSettings.pagination = {
			el: this.$element.find( '.swiper-pagination' ).get(0),
			type: 'bullets',
			clickable: true,
		};

		if ( swiperOptions ) {
			carouselSettings = _.extend( swiperOptions, carouselSettings );
		}

		return carouselSettings;
	}

	async onInit( ...args ) {
		super.onInit( ...args );

		if ( ! this.elements.$swiperContainer.length || 1 > this.elements.$slides.length ) {
			return;
		}

		const Swiper = elementorFrontend.utils.swiper;

		this.swiper = await new Swiper( this.elements.$swiperContainer, this.getCarouselOptions( true ) );

		this.elements.$swiperContainer.data( 'swiper', this.swiper );

		if ( 'yes' === this.getElementSettings( 'pause_on_hover' ) ) {
			this.togglePauseOnHover( true );
		}
	}

	onElementChange( propertyName ) {
		switch ( propertyName ) {
			case 'pause_on_hover':
				const pauseable = this.getElementSettings( 'pause_on_hover' );

				this.togglePauseOnHover( 'yes' === pauseable );
				break;

			case 'autoplay_speed':
				this.swiper.params.autoplay.delay = this.getElementSettings( 'autoplay_speed' );
				this.swiper.update();
				break;

			case 'speed':
				this.swiper.params.speed = this.getElementSettings( 'speed' );
				this.swiper.update();
				break;
		}
	}

	onEditSettingsChange( propertyName ) {
		if ( 'activeItemIndex' === propertyName ) {
			this.swiper.slideToLoop( this.getEditSettings( 'activeItemIndex' ) - 1 );
		}
	}
}

class MottaProductsListingHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				tab_heading: '.motta-products-listing__tabs span',
				tab_content: '.motta-products-listing__items',
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$tab_heading: this.findElement( selectors.tab_heading ),
			$tab_content: this.findElement( selectors.tab_content ),
		};
	}

	activateDefaultTab() {
		if( ! this.elements.$tab_heading.hasClass( 'active' ) ) {
			this.elements.$tab_heading.removeClass( 'active' );
			this.elements.$tab_content.removeClass( 'active' );

			this.elements.$tab_heading.first().addClass( 'active' );
			this.getPanel( this.elements.$tab_heading.first().attr( 'data-tabs' ) ).addClass( 'active' );
		}
	}

	changeActiveTab( tabData ) {
		if ( this.isActiveTab( tabData ) ) {
			return;
		}

		const $tab = this.getTab( tabData ),
			  $panel = this.getPanel( tabData );

		$tab.addClass( 'active' ).siblings( '.active' ).removeClass( 'active' );
		$panel.addClass( 'active' ).siblings( '.active' ).removeClass( 'active' );
	}

	isActiveTab( tabData ) {
		return this.getTab( tabData ).hasClass( 'active' );
	}

	getTab( tabData ) {
		return this.elements.$tab_heading.filter( '[data-tabs="' + tabData + '"]' );
	}

	getPanel( tabData ) {
		return this.elements.$tab_content.filter( '[data-tabs="' + tabData + '"]' );
	}

	bindEvents() {
		this.elements.$tab_heading.on( {
			click: ( event ) => {
				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-tabs' ) );
			}
		} );
	}

	onInit( ...args ) {
		super.onInit( ...args );

		this.activateDefaultTab();
	}
}

class MottaProductsRecentlyViewedWidgetHandler extends elementorModules.frontend.handlers.Base {
    getDefaultSettings() {
        return {
            selectors: {
                container: '.motta-products-recently-viewed-carousel',
                products: '.motta-products-recently-viewed__products'
            },
        };
    }

    getDefaultElements() {
        const selectors = this.getSettings( 'selectors' );

        return {
            $container: this.$element.find( selectors.container ),
            $products: this.$element.find( selectors.products )
        };
    }

    /**
     * Get Product AJAX
     */
    getProductsHandler () {
        var self = this,
            $selector = self.elements.$products;

        if ( $selector.hasClass( 'loaded' ) ) {
            return;
        }

        var elementSettings = $selector.data( 'settings' );

        if( elementSettings.load_ajax == 'yes' ){
            jQuery(window).on( 'scroll', function () {
				if (jQuery(document.body).find( '.motta-products-recently-viewed__products' ).is( ':in-viewport' )) {
					self.getProductsAJAXHandler();
				}
			}).trigger( 'scroll' );
        } else {
			self.getProductsAJAXHandler();
		}
    };

    getProductsAJAXHandler () {
        var self = this,
            $selector = self.elements.$products;

        if ( $selector.hasClass( 'loaded' ) ) {
            return;
        }

        var elementSettings = $selector.data( 'settings' ),
            ajax_url = mottaData.ajax_url.toString().replace( '%%endpoint%%', 'motta_load_recently_viewed_products' );

        jQuery.post( ajax_url, {
                settings: elementSettings
            }, ( response ) => {
                if ( ! response ) {
                    return;
                }

                var $content = jQuery( response.data ).find( 'li.product' );

                $selector.find( '.motta-products-recently-viewed__products' ).html( response.data );

                $selector.addClass( 'loaded' );

                jQuery(document.body).trigger( 'motta_products_loaded', [$content, true] );
            }
        );
    };

    onInit() {
        super.onInit();

        this.getProductsHandler();
    }
}

class MottaCounterWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				counterNumber: '.motta-counter__number'
			}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings('selectors');
		return {
		  $counterNumber: this.$element.find(selectors.counterNumber)
		};
	}

	onInit() {
		super.onInit();

		this.intersectionObserver = elementorModules.utils.Scroll.scrollObserver({
			callback: event => {
			  if (event.isInViewport) {
				this.intersectionObserver.unobserve(this.elements.$counterNumber[0]);
				const data = this.elements.$counterNumber.data(),
					  decimalDigits = data.toValue.toString().match(/\.(.*)/);

				if (decimalDigits) {
				  data.rounding = decimalDigits[1].length;
				}

				this.elements.$counterNumber.numerator(data);
			  }
			}
		  });
		  this.intersectionObserver.observe(this.elements.$counterNumber[0]);
	}
}

class MottaProductsViewWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-products-view'
			}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings('selectors');

		return {
		  $container: this.$element.find(selectors.container)
		};
	}

	productsView () {
		const container = this.elements.$container,
		products = container.closest('.site-content').find( 'ul.products' ) ;

        jQuery( '.motta-toolbar-view' ).on( 'click', 'a', function (e) {
            e.preventDefault();

            var $el = jQuery( this ),
                view = $el.data( 'view' );

            if ($el.hasClass( 'current' )) {
                return;
            }

            $el.addClass( 'current' ).siblings().removeClass( 'current' );

			container.closest('body').removeClass('catalog-view-grid-2 catalog-view-grid-3 catalog-view-default catalog-view-list').addClass('catalog-view-' + view);

			if ( container.closest('body').hasClass( 'catalog-view-grid-2' ) ) {
				products.removeClass( 'columns-3 columns-4').addClass( 'columns-2'  );
			} else if ( container.closest('body').hasClass( 'catalog-view-grid-3' ) ) {
				products.removeClass( 'columns-2 columns-4').addClass( 'columns-3' );
			} else if( container.closest('body').hasClass( 'catalog-view-default' ) ) {
				products.removeClass( 'columns-2 columns-3' ).addClass( 'columns-4' );
			} else if( container.closest('body').hasClass( 'catalog-view-list' ) ) {
				products.removeClass( 'columns-2 columns-3 columns-4' ).addClass( 'columns-1' );
			}

        });

        this.productViewSwitch();
    }

	productViewSwitch () {
		const container = this.elements.$container,
		products = container.closest('.site-content').find( 'ul.products' ) ;

        if ( container.closest('body').hasClass( 'catalog-view-grid-2' ) ) {
			products.removeClass( 'columns-3 columns-4').addClass( 'columns-2'  );
		} else if ( container.closest('body').hasClass( 'catalog-view-grid-3' ) ) {
			products.removeClass( 'columns-2 columns-4').addClass( 'columns-3' );
        } else if( container.closest('body').hasClass( 'catalog-view-default' ) ) {
			products.removeClass( 'columns-2 columns-3' ).addClass( 'columns-4' );
        } else if( container.closest('body').hasClass( 'catalog-view-list' ) ) {
			products.removeClass( 'columns-2 columns-3 columns-4' ).addClass( 'columns-1' );
        }
    }

	onInit() {
		super.onInit();

		this.productsView();
	}
}

class MottaImageBeforeAfterWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-image-before-after'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	changeImagesHandle() {
		const container = this.elements.$container;

        container.imagesLoaded( function () {
            container.find( '.box-thumbnail' ).imageslide();
        } );
	}

	onInit() {
		super.onInit();

		if ( ! this.elements.$container.length ) {
			return;
		}
		this.changeImagesHandle();
	}
}

class MottaProduct360ViewerWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-360-degree-viewer'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	getImagesDegree() {
		const $container = this.elements.$container;

		if ( ! $container.length ) {
			return;
		}

		var options = $container.data('options');
		var degree = $container.find('.motta-images-gallery-degree').ThreeSixty({
			totalFrames: options['total_frames'], // Total no. of image you have for 360 slider
			endFrame: options['total_frames'], // end frame for the auto spin animation
			currentFrame: 1, // This the start frame for auto spin
			imgList: $container.find('.product-degree__images'), // selector for image list
			progress: '.motta-gallery-degree__spinner', // selector to show the loading progress
			imgArray: options['images'], // path of the image assets
			height: options['height'],
			width: options['width'],
			navigation: true,
			responsive: true
		});

		$container.on('click', '.nav-bar__run', function () {
			jQuery(this).addClass('active');
			degree.play();
		});

		$container.on('click', '.nav-bar__run.active', function () {
			jQuery(this).removeClass('active');
			degree.stop();
		});

		$container.on('click', '.nav-bar__next', function () {
			degree.stop();
			jQuery('.nav-bar__run').removeClass('active');
			degree.next();
		});

		$container.on('click', '.nav-bar__prev', function () {
			degree.stop();
			jQuery('.nav-bar__run').removeClass('active');
			degree.previous();
		});
	}

	onInit() {
		super.onInit();
	 	window.onload =	this.getImagesDegree();
	}
}

class MottaGalleryWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-gallery--elementor'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	getMasonryInit() {
		if ( ! this.elements.$container.closest('.elementor-widget-motta-gallery').hasClass('motta-gallery__masonry--yes') ) {
			return;
		}

		if( jQuery('body').hasClass('elementor-editor-active') ) {
			return;
		}

		const gallery = this.elements.$container;
		gallery.imagesLoaded( function () {
			gallery.masonry({
				itemSelector: '.gallery-item',
				percentPosition: true,
				horizontalOrder: true
			});
		});
	}

	onInit() {
		super.onInit();
		this.getMasonryInit();
	}
}

class MottaImageHotspotWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				container: '.motta-image-hotspot'
			},
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$container: this.$element.find( selectors.container )
		};
	}

	hostpotHandle(){
		const 	container = this.elements.$container;
		const 	item = container.find('.motta-hotspot-item');

		if ( ! this.elements.$container.length ) {
			return;
		}

        container.on('click', '.motta-hotspot-item .motta-hotspot__point', function (e) {
            var el = jQuery(this).closest('.motta-hotspot-item'),
                siblings = el.siblings();

            el.toggleClass('active');
            siblings.removeClass('active');
        });

        jQuery(document.body).on('click', function (evt) {

            if (jQuery(evt.target).closest(item).length > 0) {
                return;
            }

            item.removeClass('active');
        });
	}

	onInit() {
		super.onInit();
		this.hostpotHandle();
	}
}

class MottaTabsHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				tab: '.motta-tab__title',
				panel: '.motta-tab__content'
			},
			classes: {
				active: 'motta-tab--active',
			},
			showFn: 'show',
			hideFn: 'hide',
			toggleSelf: false,
			autoExpand: true,
			hidePrevious: true
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );

		return {
			$tabs: this.findElement( selectors.tab ),
			$panels: this.findElement( selectors.panel )
		};
	}

	activateDefaultTab() {
		const settings = this.getSettings();

		if ( ! settings.autoExpand || 'editor' === settings.autoExpand && ! this.isEdit ) {
			return;
		}

		const defaultActiveTab = this.getEditSettings( 'activeItemIndex' ) || 1,
			originalToggleMethods = {
				showFn: settings.showFn,
				hideFn: settings.hideFn
			};

		this.setSettings( {
			showFn: 'show',
			hideFn: 'hide'
		} );

		this.changeActiveTab( defaultActiveTab );

		this.setSettings( originalToggleMethods );
	}

	changeActiveTab( tabIndex ) {
		const settings = this.getSettings(),
			$tab = this.elements.$tabs.filter( '[data-tab="' + tabIndex + '"]' ),
			$panel = this.elements.$panels.filter( '[data-tab="' + tabIndex + '"]' ),
			isActive = $tab.hasClass( settings.classes.active );

		if ( ! settings.toggleSelf && isActive ) {
			return;
		}

		if ( ( settings.toggleSelf || ! isActive ) && settings.hidePrevious ) {
			this.elements.$tabs.removeClass( settings.classes.active );
			this.elements.$panels.removeClass( settings.classes.active )[settings.hideFn]();
		}

		if ( ! settings.hidePrevious && isActive ) {
			$tab.removeClass( settings.classes.active );
			$panel.removeClass( settings.classes.active )[settings.hideFn]();
		}

		if ( ! isActive ) {
			$tab.addClass( settings.classes.active );
			$panel.addClass( settings.classes.active )[settings.showFn]();
		}
	}

	bindEvents() {
		this.elements.$tabs.on( {
			keydown: ( event ) => {
				if ( 'Enter' !== event.key ) {
					return;
				}

				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-tab' ) );
			},
			click: ( event ) => {
				event.preventDefault();

				this.changeActiveTab( event.currentTarget.getAttribute( 'data-tab' ) );
			}
		} );
	}

	onInit() {
		super.onInit();

		this.activateDefaultTab();
	}
}

class MottaAlertHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
		  	selectors: {
				dismissButton: '.motta-alert__dismiss'
		 	}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings('selectors');
		return {
			$dismissButton: this.$element.find(selectors.dismissButton)
		};
	}

	bindEvents() {
		this.elements.$dismissButton.on('click', this.onDismissButtonClick.bind(this));
	}

	onDismissButtonClick() {
		this.$element.fadeOut();
	}
}

class MottaShareSocialsHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
		  	selectors: {
				copyButton: '.motta-share-icons__copylink--button',
				linkURL: '.motta-share-icons__copylink--link'
		 	}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings('selectors');
		return {
			$copyButton: this.$element.find(selectors.copyButton),
			$linkURL: this.$element.find(selectors.linkURL)
		};
	}

	bindEvents() {
		this.elements.$copyButton.on('click', this.onCopyButtonClick.bind(this));
	}

	onCopyButtonClick(e) {
		e.preventDefault();

		this.elements.$linkURL.select();
		navigator.clipboard.writeText(this.elements.$linkURL.val());
	}
}

class MottaStoreLocationsWidgetHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				grid: '.motta-store-locations__wrapper',
				filter: '.motta-store-locations__tags',
			}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );
		return {
			$grid: this.findElement( selectors.grid ),
			$filter: this.findElement( selectors.filter ),
		};
	}

	bindEvents(){
		jQuery( '.motta-store-locations__tags-item' ).on( 'click', function() {
			var $this = jQuery( this ),
				tags = $this.data( 'tag' ),
				wrapper = $this.parent().siblings( '.motta-store-locations__wrapper' );

			$this.parent().find( '.motta-store-locations__tags-item' ).removeClass( 'active' );
			$this.addClass( 'active' );

			if( tags ) {
				wrapper.find( '.motta-store-locations__item' ).not( tags ).removeClass( 'fadeIn' ).addClass( 'fadeOut' );
				wrapper.find( '.motta-store-locations__item' + tags ).removeClass( 'fadeIn fadeOut' ).addClass( 'fadeIn' );
			}

			if( wrapper.find( '.motta-store-locations__item' ).hasClass( 'fadeIn' ) ){
				setTimeout( function(){
					wrapper.find( '.motta-store-locations__item' + tags ).removeClass( 'fadeIn' );
				}, 310 )
			}
		});
	}

	onInit() {
		super.onInit();
	}
}

class MottaGoogleMapHandler extends elementorModules.frontend.handlers.Base {
	getDefaultSettings() {
		return {
			selectors: {
				map: '.motta-google-map__wapper',
				markers: '.motta-google-map__markers',
				search: '.motta-gm-search__field'
			}
		};
	}

	getDefaultElements() {
		const selectors = this.getSettings( 'selectors' );
		return {
			$map: this.$element.find( selectors.map ),
			$markers: this.$element.find( selectors.markers ),
			$search: this.$element.find( selectors.search )
		};
	}

	hasLocation( address ) {
		this.locations = this.locations || [];
		address = address.trim();

		let found = this.locations.filter( location => location.address === address );

		if ( ! found.length ) {
			return false;
		}

		return found[0].location;
	}

	getLocation( address ) {
		this.locations = this.locations || [];
		address = address.trim();

		if ( ! address ) {
			return false;
		}

		let location = this.hasLocation( address );

		if ( location ) {
			return location.location;
		}

		return new Promise( (resolve, reject) => {
			const geocoder = new google.maps.Geocoder;

			geocoder.geocode( { address: address }, (results, status) => {
				if ( status === 'OK' ) {
					if ( results[0] ) {
						this.locations.push( {
							address: address,
							location: results[0].geometry.location
						} );

						resolve( results[0].geometry.location );
					} else {
						reject( 'No address found' );
					}
				} else {
					reject( status );
				}
			} )
		} );
	}

	async getMapOptions() {
		const settings = this.getElementSettings();
		const location = this.elements.$map.data( 'location' );
		const options = {
			scrollwheel      : false,
			navigationControl: true,
			mapTypeControl   : false,
			scaleControl     : false,
			streetViewControl: false,
			draggable        : true,
			mapTypeId        : google.maps.MapTypeId.ROADMAP,
			zoom             : settings.zoom.size
		};

		if ( location ) {
			options.center = location;
		} else {
			let latlng = settings.latlng.split( ',' ).map( parseFloat );

			if ( latlng.length > 1 && ! Number.isNaN( latlng[0] ) && ! Number.isNaN( latlng[1] ) ) {
				options.center = {
					lat: latlng[0],
					lng: latlng[1]
				};
			}
		}

		if ( ! options.center ) {
			options.center = await this.getLocation( settings.address );
		}

		return options;
	}

	async initMap() {
		if ( ! this.elements.$map.length ) {
			return;
		}

		if ( this.map ) {
			return;
		}

		this.map = new google.maps.Map( this.elements.$map.get( 0 ), await this.getMapOptions() );
	}

	async setMapLocation() {
		if ( ! this.isEdit ) {
			return;
		}

		if ( ! this.elements.$map.length ) {
			return;
		}

		if ( typeof this.map === 'undefined' ) {
			return;
		}

		const settings = this.getElementSettings();
		let location = {};
		let latlng = settings.latlng.split( ',' ).map( parseFloat );

		if ( latlng.length > 1 && ! Number.isNaN( latlng[0] ) && ! Number.isNaN( latlng[1] ) ) {
			location = {
				lat: latlng[0],
				lng: latlng[1]
			};
		} else {
			location = await this.getLocation( settings.address );
		}

		if ( location ) {
			this.map.setCenter( location );
		}
	}

	clearMarkers() {
		if ( this.markers ) {
			for ( let i in this.markers ) {
				this.markers[i].setMap( null );
			}
		}

		this.markers = [];
	}

	async updateLocationList() {
		if ( ! this.elements.$markers.length ) {
			return;
		}

		const addresses = {
			name: [],
			latlng: []
		};

		this.elements.$markers.children().each( ( index, marker ) => {
			let data = JSON.parse( marker.dataset.marker );
			let address = data.address;

			if ( ! address ) {
				return;
			}

			if ( this.hasLocation( address ) ) {
				return;
			}

			let latlng = data.latlng.split( ',' ).map( parseFloat );

			if ( latlng.length > 1 && ! Number.isNaN( latlng[0] ) && ! Number.isNaN( latlng[1] ) ) {
				return;
			}

			addresses.name.push( address );
			addresses.latlng.push( this.getLocation( address ) );
		} );

		await Promise.all( addresses.latlng ).then( coordinates => {
			for ( let i in coordinates ) {
				if ( ! this.hasLocation( addresses.name[i] ) ) {
					this.locations.push( {
						address: addresses.name[i],
						location: coordinates[i]
					} );
				}
			}
		} ).catch( error => {
			console.warn( error );
		} );
	}

	async updateMarkers() {
		if ( typeof this.map === 'undefined' ) {
			return;
		}

		if ( ! this.elements.$markers.length ) {
			return;
		}

		// Reset all markers.
		this.clearMarkers();

		// Update locations.
		await this.updateLocationList();

		this.elements.$markers.children().each( ( index, marker ) => {
			let data = JSON.parse( marker.dataset.marker );
			let markerOptions = {
				map: this.map,
				animation: google.maps.Animation.DROP
			}

			if ( data.icon.url ) {
				markerOptions.icon = data.icon.url;
			}

			let latlng = data.latlng.split( ',' ).map( parseFloat );

			if ( latlng.length > 1 && ! Number.isNaN( latlng[0] ) && ! Number.isNaN( latlng[1] ) ) {
				markerOptions.position = {
					lat: latlng[0],
					lng: latlng[1]
				};
			} else if ( data.address ) {
				markerOptions.position = this.hasLocation( data.address )
			} else {
				return;
			}

			let mapMarker = new google.maps.Marker( markerOptions );

			if ( marker.innerHTML ) {
				let infoWindow = new google.maps.InfoWindow( {
					content: '<div class="motta-google-map__info info_content">' + marker.innerHTML + '</div>'
				} );

				mapMarker.addListener( 'click', () => {
					infoWindow.open( this.map, mapMarker );
				} );
			}

			this.markers.push( mapMarker );
		} );
	}

	async findMapLocation() {
		var el = this;
		this.elements.$markers.on( 'click', '.motta-google-map__marker', function( e ) {
			e.preventDefault();
			jQuery(this).siblings().removeClass('motta-open').find('.motta-google-map__marker--foot').slideUp();
			jQuery(this).toggleClass('motta-open');
			jQuery(this).find('.motta-google-map__marker--foot').slideToggle();
			var data =  jQuery(this).data('marker');
			var location = {};
			var latlng = data.latlng.split( ',' ).map( parseFloat );

			if ( latlng.length > 1 && ! Number.isNaN( latlng[0] ) && ! Number.isNaN( latlng[1] ) ) {
				location = {
					lat: latlng[0],
					lng: latlng[1]
				};
			} else if ( data.address ) {
				location =  el.hasLocation( data.address );
			}

			if ( location ) {
				el.map.panTo( location );
			}
		} );
	}

	async searchMapLocation() {
		this.elements.$search.on('input', function() {
			var $this = jQuery( this ),
			term = $this.val().toLowerCase(),
			$list = $this.closest('.motta-google-map--header').next( '.motta-google-map' ).find( '.motta-google-map__marker' );

			if ( term ) {
				$list.hide().filter( function() {
					return jQuery( '.motta-google-map__marker--head h4', this ).text().toLowerCase().indexOf( term ) !== -1;
				} ).show();
			} else {
				$list.show();
			}
		});
	}

	async onInit() {
		super.onInit();

		try {
			await this.initMap();
			await this.setMapLocation();
			this.updateMarkers();
			this.findMapLocation();
			this.searchMapLocation();
		} catch ( error ) {
			console.warn( error );
		}
	}

	async onElementChange( propertyName ) {
		if ( 'address' === propertyName || 'latlng' === propertyName ) {
			clearTimeout( this.timerAddressChange );
			this.timerAddressChange = setTimeout( () => {
				this.setMapLocation();
			}, 1000 );
		}

		if ( 'zoom' === propertyName ) {
			let zoom = this.getElementSettings( 'zoom' );

			this.map.setZoom( zoom.size );
		}

		if ( 'color' === propertyName ) {
			this.map.setOptions( {
				styles: this.getMapStyleOption()
			} );
		}
	}
}

class MottaProductGridHandler extends elementorModules.frontend.handlers.Base {
    getDefaultSettings() {
        return {
            selectors: {
                container: '.motta-product-grid'
            },
        };
    }

    getDefaultElements() {
        const selectors = this.getSettings('selectors');

        return {
            $container: this.$element.find(selectors.container)
        };

    }

    loadProductsGrid() {
		var ajax_url = wc_add_to_cart_params ? wc_add_to_cart_params.wc_ajax_url.toString().replace(  '%%endpoint%%', 'motta_elementor_load_products' ) : mottaData.ajax_url;
        const settings = this.getElementSettings();

        // Load Products
        this.elements.$container.on('click', 'a.ajax-load-products', function (e) {
            e.preventDefault();

            var $el = jQuery(this);

            if ($el.hasClass('loading')) {
                return;
            }

            $el.addClass('loading');

            jQuery.post(
                ajax_url,
                {
                    page: $el.attr('data-page'),
                    settings: settings
                },
                function (response) {
                    if (!response) {
                        return;
                    }

                    $el.removeClass('loading');

                    var $data = jQuery(response.data),
                        $products = $data.find('li.product'),
                        $container = $el.closest('.motta-product-grid'),
                        $grid = $container.find('ul.products'),
                        $page_number = $data.find('.page-number').data('page');

                    // If has products
                    if ($products.length) {
						$products.addClass( 'animated mottaFadeInUp' );

                        $grid.append($products);

                        if ($page_number == '0') {
                            $el.remove();
                        } else {
                            $el.attr('data-page', $page_number);
                        }
                    }

                    jQuery(document.body).trigger('motta_products_loaded', [$products, true]);
                }
            );
        });
    };

    loadProductsInfinite() {
        if (!this.elements.$container.find('.ajax-load-products').hasClass('ajax-infinite')) {
            return;
        }
		var waiting = false,
		endScrollHandle;

        var self = this;

		jQuery( window ).on( 'scroll', function() {
			if ( waiting ) {
				return;
			}

			waiting = true;

			clearTimeout( endScrollHandle );

			self.infiniteScoll();

			setTimeout( function() {
				waiting = false;
			}, 100 );

			endScrollHandle = setTimeout( function() {
				waiting = false;
				self.infiniteScoll();
			}, 200 );
		});
    };

	infiniteScoll() {
		var $navigation = this.elements.$container.find('.ajax-load-products' );

		if ( this.isVisible( $navigation )  ) {
			$navigation.trigger('click');
		}
	}

	 isVisible( el ) {
		if ( el instanceof jQuery ) {
			el = el[0];
		}

		if ( ! el ) {
			return false;
		}

		var rect = el.getBoundingClientRect();

		return rect.bottom > 0 &&
			rect.right > 0 &&
			rect.left < (window.innerWidth || document.documentElement.clientWidth) &&
			rect.top < (window.innerHeight || document.documentElement.clientHeight);
	};

    onInit() {
        var self = this;
        super.onInit();

        self.loadProductsGrid();
        self.loadProductsInfinite();
    }
}

jQuery( window ).on( 'elementor/frontend/init', () => {
	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-navigation-menu.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaNavigationMenuHandler, { $element } );
	} );
	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-countdown.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaCountDownWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-team-member-grid.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaTeamMemberGridWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-accordion.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaAccordionWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-slides.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSlidesWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-banner.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaBannerImageWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-countdown-banner.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaCountDownBannerWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-tabs.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductsTabsHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-tabs-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductsTabsHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				carousel: '.motta-product-carousel .woocommerce',
				slideContent: '.product',
			},
			carouselContent: 'products',
			swiperOptions: {
				wrapperClass: 'products',
				slideClass: 'product',
				watchSlidesProgress: true,
			},
		} );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-product-deals.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				carousel: '.motta-product-deals__products',
				slideContent: '.product',
			},
			carouselContent: 'products',
			swiperOptions: {
				wrapperClass: 'products',
				slideClass: 'product',
				spaceBetween: 0,
			},
		} );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-listing.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductsListingHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-recently-viewed.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductsRecentlyViewedWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-recently-viewed-carousel.default', ( $element ) => {
		if ( $element.find('ul.products li').hasClass('no-products') ) {
			$element.find('.motta-products-recently-viewed__heading').hide();
			$element.find('.motta-product-carousel').hide();
			$element.find('.motta-swiper-button').hide();
			$element.find('.swiper-pagination').hide();
		}

		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				carousel: '.motta-product-carousel .woocommerce',
				slideContent: '.product',
			},
			carouselContent: 'products',
			swiperOptions: {
				wrapperClass: 'products',
				slideClass: 'product',
			},
		} );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-counter.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaCounterWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-navigation-bar.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaNavigationBarWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-products-view.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductsViewWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-image-before-after.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaImageBeforeAfterWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-posts-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-images-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-testimonial-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-image-box-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-team-member-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-product-360-viewer.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProduct360ViewerWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-icons-box-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-gallery.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaGalleryWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-image-hotspot.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaImageHotspotWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-gallery-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				slideContent: '.gallery-item',
			},
			carouselContent: 'gallery',
			swiperOptions: {
				wrapperClass: 'gallery',
				slideClass: 'gallery-item',
			},
		} );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-tabs.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaTabsHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-instagram-carousel.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaInstagramCarouselWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-alert.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaAlertHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-share-socials.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaShareSocialsHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-store-locations.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaStoreLocationsWidgetHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-google-map.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaGoogleMapHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-product-grid.default', ( $element ) => {
		elementorFrontend.elementsHandler.addHandler( MottaProductGridHandler, { $element } );
	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-wc-product-related.default', ( $element ) => {
        var $products = $element.find('ul.products');
        $products.after('<div class="swiper-pagination"></div>');
        $products.after('<span class="motta-svg-icon swiper-button motta-swiper-button-prev motta-swiper-button"><svg viewBox="0 0 32 32"><path d="M20.58 2.58l2.84 2.84-10.6 10.58 10.6 10.58-2.84 2.84-13.4-13.42z"></path></svg></span>');
        $products.after('<span class="motta-svg-icon swiper-button motta-swiper-button-next motta-swiper-button"><svg viewBox="0 0 32 32"><path d="M11.42 29.42l-2.84-2.84 10.6-10.58-10.6-10.58 2.84-2.84 13.4 13.42z"></path></svg></span>');
        $products.wrap('<div class="swiper-container linked-products-carousel"></div>');
        $products.addClass('swiper-wrapper');
        $products.find('li.product').addClass('swiper-slide');

		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				carousel: '.linked-products-carousel',
				slideContent: '.product',
			},
			carouselContent: 'products',
			swiperOptions: {
				wrapperClass: 'products',
				slideClass: 'product',
				watchSlidesProgress: true,
			},
		} );

	} );

	elementorFrontend.hooks.addAction( 'frontend/element_ready/motta-wc-product-upsell.default', ( $element ) => {
        var $products = $element.find('ul.products');
        $products.after('<div class="swiper-pagination"></div>');
        $products.after('<span class="motta-svg-icon swiper-button motta-swiper-button-prev motta-swiper-button"><svg viewBox="0 0 32 32"><path d="M20.58 2.58l2.84 2.84-10.6 10.58 10.6 10.58-2.84 2.84-13.4-13.42z"></path></svg></span>');
        $products.after('<span class="motta-svg-icon swiper-button motta-swiper-button-next motta-swiper-button"><svg viewBox="0 0 32 32"><path d="M11.42 29.42l-2.84-2.84 10.6-10.58-10.6-10.58 2.84-2.84 13.4 13.42z"></path></svg></span>');
        $products.wrap('<div class="swiper-container linked-products-carousel"></div>');
        $products.addClass('swiper-wrapper');
        $products.find('li.product').addClass('swiper-slide');

		elementorFrontend.elementsHandler.addHandler( MottaSwiperCarouselWidgetHandler, {
			$element: $element,
			selectors: {
				carousel: '.linked-products-carousel',
				slideContent: '.product',
			},
			carouselContent: 'products',
			swiperOptions: {
				wrapperClass: 'products',
				slideClass: 'product',
				watchSlidesProgress: true,
			},
		} );

	} );

} );
