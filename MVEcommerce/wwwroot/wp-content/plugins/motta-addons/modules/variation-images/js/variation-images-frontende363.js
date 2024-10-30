(function ($) {
    'use strict';
    var motta = motta || {};

    motta.found_data = false;
    motta.variation_id = 0;

    motta.foundVariationImages = function( ) {
        $( '.variations_form:not(.form-cart-pbt)' ).on('found_variation', function(e, $variation){
            if( motta.variation_id != $variation.variation_id ) {
                motta.changeVariationImagesAjax($variation.variation_id, $(this).data('product_id'));
                motta.found_data = true;
                motta.variation_id = $variation.variation_id;
            }
        });
    }

    motta.resetVariationImages = function( ) {
        $( '.variations_form:not(.form-cart-pbt)' ).on('reset_data', function(e){
            if( motta.found_data ) {
                motta.changeVariationImagesAjax(0, $(this).data('product_id'));
                motta.found_data = false;
                motta.variation_id = 0;
            }

        });
    }

    motta.changeVariationImagesAjax = function(variation_id, product_id) {
        var $productGallery = $('.woocommerce-product-gallery'),
            galleryHeight = $productGallery.height();
            $productGallery.addClass('loading').css( {'overflow': 'hidden' });
            if( ! $productGallery.closest('.single-product').hasClass('quick-view-modal') ) {
                $productGallery.css( {'height': galleryHeight });
            }

        var data = {
            'variation_id': variation_id,
            'product_id': product_id,
            nonce: mottaData.nonce,
        },
        ajax_url = mottaData.ajax_url.toString().replace('%%endpoint%%', 'motta_get_variation_images');

        var xhr = $.post(
            ajax_url,
            data,
            function (response) {
                var $gallery = $(response.data);

                $productGallery.html( $gallery.html() );
                if ( typeof wc_single_product_params !== 'undefined' && $.fn.wc_product_gallery) {
                    $productGallery.removeData('flexslider');
                    $productGallery.off('click', '.woocommerce-product-gallery__image a');
                    $productGallery.off('click', '.woocommerce-product-gallery__trigger');
                    $productGallery.wc_product_gallery( wc_single_product_params );

                }
                $productGallery.trigger('motta_update_product_gallery_on_quickview');

                $productGallery.imagesLoaded(function () {
                    setTimeout(function() {
                        $productGallery.removeClass('loading').removeAttr( 'style' ).css('opacity', '1');
                    }, 200);

                } );

                $productGallery.trigger('product_thumbnails_slider_horizontal');
                $productGallery.trigger('product_thumbnails_slider_vertical');
                $productGallery.trigger( 'motta_gallery_init', $productGallery.find('.woocommerce-product-gallery__image').first());

            }
        );
    }
    /**
     * Document ready
     */
    $(function () {
        if( $('div.product' ).hasClass('product-has-variation-images') ) {
            motta.foundVariationImages();
            motta.resetVariationImages();
        }

        $('body').on( 'motta_product_quick_view_loaded', function() {
            if( $('div.product' ).hasClass('product-has-variation-images') ) {
                motta.foundVariationImages();
                motta.resetVariationImages();
            }
        } );
    });

})(jQuery);