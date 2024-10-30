"use strict";
var woocs_loading_first_time = true;//simply flag var
var woocs_sumbit_currency_changing = true;//just a flag variable for drop-down redraws when prices redraws by AJAX

jQuery(function ($) {

    if (typeof woocs_array_of_get === 'undefined') {
        return;//to avoid notices and errors
    }

    woocs_array_of_get = JSON.parse(woocs_array_of_get);

    //wp-content\plugins\woocommerce\assets\js\frontend\cart.js
    if (Object.keys(woocs_array_of_get).length !== 0) {
        if ('currency' in woocs_array_of_get) {
            //this code nessesary for correct redrawing of the shipping methods while currency changes on the cart page
            $('body.woocommerce-cart .shop_table.cart').closest('form').find('input[name="update_cart"]').prop('disabled', false);
            $('body.woocommerce-cart .shop_table.cart').closest('form').find('input[name="update_cart"]').trigger('click');
        }
    }

    //keeps data of $_GET array

    if (Object.keys(woocs_array_of_get).length == 0) {
        woocs_array_of_get = {};
    }

    //***
    woocs_array_no_cents = JSON.parse(woocs_array_no_cents);

    //***

    //emptying cart widget after checkout
    if (!parseInt(woocs_get_cookie('woocommerce_items_in_cart'), 10)) {
        $('.widget_shopping_cart_content').empty();
        $(document.body).trigger('wc_fragment_refresh');
    }

    if (woocs_array_of_get.currency != undefined || woocs_array_of_get.removed_item != undefined || woocs_array_of_get.key != undefined)
    {
        woocs_refresh_mini_cart(555);
    }
    if (woocs_redraw_cart) {
        //intercept adding to cart event to redraw mini-cart widget
        jQuery(document).on("adding_to_cart", function () {
            woocs_refresh_mini_cart(999);
        });
    }

    //to make price popup mobile friendly
    jQuery('body').on('click', '.woocs_price_info', function () {
        return false;
    });

    //+++++++++++++++++++++++++++++++++++++++++++++++

    if (woocs_drop_down_view == 'chosen' || woocs_drop_down_view == 'chosen_dark') {
        try {
            if (jQuery("select.woocommerce-currency-switcher").not('.woocs_clean_select').length) {
                jQuery("select.woocommerce-currency-switcher").not('.woocs_clean_select').chosen({
                    disable_search_threshold: 10
                });

                jQuery.each(jQuery('.woocommerce-currency-switcher-form .chosen-container'), function (index, obj) {
                    jQuery(obj).css({'width': jQuery(this).prev('select').data('width')});
                });
            }
        } catch (e) {
            console.log(e);
        }
    }



    if (woocs_drop_down_view == 'ddslick') {
        try {
            woocs_init_ddslick_select();

        } catch (e) {
            console.log(e);
        }
    }

    woocs_loading_first_time = false;


    if (woocs_drop_down_view == 'wselect' && woocs_is_mobile != 1) {
        try {
            //https://github.com/websanova/wSelect#wselectjs
            jQuery('select.woocommerce-currency-switcher').not('.woocs_clean_select').wSelect({
                size: 7
            });
        } catch (e) {
            console.log(e);
        }
    }

    //for flags view instead of drop-down
    jQuery('.woocs_flag_view_item').on("click", function () {
        if (woocs_sumbit_currency_changing) {
            if (jQuery(this).hasClass('woocs_flag_view_item_current')) {
                return false;
            }
            //***

            if (Object.keys(woocs_array_of_get).length == 0) {
                window.location = window.location.href + '?currency=' + jQuery(this).data('currency');
            } else {

                woocs_redirect(jQuery(this).data('currency'));

            }
        }

        return false;
    });

    //for converter
    if (jQuery('.woocs_converter_shortcode').length) {
        jQuery('.woocs_converter_shortcode_button').on("click", function () {
            var amount = jQuery(this).parent('.woocs_converter_shortcode').find('.woocs_converter_shortcode_amount').eq(0).val();
            var from = jQuery(this).parent('.woocs_converter_shortcode').find('.woocs_converter_shortcode_from').eq(0).val();
            var to = jQuery(this).parent('.woocs_converter_shortcode').find('.woocs_converter_shortcode_to').eq(0).val();
            var precision = jQuery(this).parent('.woocs_converter_shortcode').find('.woocs_converter_shortcode_precision').eq(0).val();
            var results_obj = jQuery(this).parent('.woocs_converter_shortcode').find('.woocs_converter_shortcode_results').eq(0);
            jQuery(results_obj).val(woocs_lang_loading + ' ...');
            var data = {
                action: "woocs_convert_currency",
                amount: amount,
                from: from,
                to: to,
                precision: precision
            };

            jQuery.post(woocs_ajaxurl, data, function (value) {
                jQuery(results_obj).val(value);
            });

            return false;

        });
    }

    //for rates
    if (jQuery('.woocs_rates_shortcode').length) {
        jQuery('body').on('change', '.woocs_rates_current_currency', function () {
            var _this = this;
            var data = {
                action: "woocs_rates_current_currency",
                current_currency: jQuery(this).val(),
                precision: jQuery(this).data('precision'),
                exclude: jQuery(this).data('exclude')
            };

            jQuery.post(woocs_ajaxurl, data, function (html) {
                jQuery(_this).parent('.woocs_rates_shortcode').html(html);
            });

            return false;

        });
    }

    //if we using js price update while the site is cached
    if (typeof woocs_shop_is_cached !== 'undefined') {
        if (woocs_shop_is_cached) {

            setTimeout(function () {

                woocs_sumbit_currency_changing = false;
                if (typeof woocs_array_of_get.currency === 'undefined') {

                    if (jQuery('body').hasClass('single')) {
                       // jQuery('.woocs_price_info').remove();
                    }

                    /****/
                    var custom_prices = [];
                    jQuery.each(jQuery('.woocs_amount_custom_price'), function (index, item) {
                        let decimals = jQuery(item).data('decimals');
                        let value = jQuery(item).data('value');
                        let currency = jQuery(item).data('currency');
                        custom_prices.push({
                            decimals: decimals,
                            value: value,
                            currency: currency
                        });
                    });
                    if (custom_prices.length != 0) {

                        var data = {
                            action: "woocs_get_custom_price_html",
                            custom_prices: custom_prices,
                            current_currency: woocs_current_currency['name']
                        };
                        jQuery.post(woocs_ajaxurl, data, function (data) {
                            data = JSON.parse(data);
                            if (!jQuery.isEmptyObject(data)) {
                                jQuery.each(data, function (val, price) {
                                    jQuery(".woocs_amount_custom_price[data-value='" + val + "']").replaceWith(price);
                                });

                            }
                        });
                    }


                    /****/

                    var variation_ids = [];
                    var var_data = jQuery("form.variations_form").data("product_variations");
                    if (typeof var_data != "undefined") {
                        jQuery.each(var_data, function (indx, attr) {
                            variation_ids.push(attr['variation_id']);
                        });
                        if (variation_ids.length != 0) {
                            var data_var = {
                                action: "woocs_get_variation_products_price_html",
                                var_products_ids: variation_ids,
                                current_currency: woocs_current_currency['name']
                            };

                            jQuery.post(woocs_ajaxurl, data_var, function (data) {
                                data = JSON.parse(data);

                                if (!jQuery.isEmptyObject(data)) {
                                    jQuery.each(var_data, function (indx, attr) {
                                        if (typeof data[attr['variation_id']] != "undefined") {
                                            var_data[indx]['price_html'] = data[attr['variation_id']];
                                        }
                                    });
                                    jQuery("form.variations_form").data("product_variations", var_data);
                                }
                            });
                        }
                    }

                    //***
                    var products_ids = {};
                    var products_currency = {};
                    jQuery.each(jQuery('.woocs_price_code'), function (index, item) {
                        products_ids[jQuery(item).data('redraw-id')] = jQuery(item).data('product-id');
			if (jQuery(item).data('currency')) {
			    products_currency[jQuery(item).data('redraw-id')] = jQuery(item).data('currency');
			}
                        
                    });

                    //if no prices on the page - do nothing
                    if (Object.keys(products_ids).length === 0) {
                        woocs_sumbit_currency_changing = true;
                        return;
                    }

                    var data = {
                        action: "woocs_get_products_price_html",
                        products_ids: products_ids,
                        products_currency: products_currency,
                        current_currency: woocs_current_currency['name']
                    };

                    jQuery.post(woocs_ajaxurl, data, function (data) {

                        data = JSON.parse(data);
                        if (jQuery.isEmptyObject(data)) {
                            woocs_sumbit_currency_changing = true;
                        }
                        if (!jQuery.isEmptyObject(data)) {
                            jQuery('.woocs_price_info').remove();
                            jQuery('.woocs_price_approx.woocs_cart_item_price').remove();
                            jQuery.each(jQuery('.woocs_price_code'), function (index, item) {
                                if (data.ids[jQuery(item).data('redraw-id')] != undefined) {
                                    jQuery(item).replaceWith(data.ids[jQuery(item).data('redraw-id')]);
                                }


                            });
                            jQuery('.woocs_price_code').removeClass('woocs_preloader_ajax');
                            //***
                            jQuery('.woocommerce-currency-switcher').val(data.current_currency);
                            //***
                            if (woocs_drop_down_view == 'chosen' || woocs_drop_down_view == 'chosen_dark') {
                                try {
                                    if (jQuery("select.woocommerce-currency-switcher").not('.woocs_clean_select').length) {
                                        jQuery("select.woocommerce-currency-switcher").not('.woocs_clean_select').chosen({
                                            disable_search_threshold: 10
                                        });
                                        jQuery('select.woocommerce-currency-switcher').trigger("chosen:updated");
                                    }
                                } catch (e) {
                                    console.log(e);
                                }
                            }
                            if (typeof data.currency_data != "undefined") {
                                woocs_current_currency = data.currency_data;
                                /* Price  slider */
                                var min = jQuery('.price_slider_amount #min_price').val();
                                var max = jQuery('.price_slider_amount #max_price').val();
                                if (typeof max != 'undefined' && typeof min != 'undefined') {
                                    max = woocs_convert_price_slider(max);
                                    min = woocs_convert_price_slider(min);
                                    jQuery(document.body).trigger('price_slider_create', [min, max]);
                                }
                            }
                            //***
                            if (woocs_drop_down_view == 'ddslick') {
                                try {
                                    jQuery('div.dd-container').ddslick('destroy');
                                    jQuery('select.woocommerce-currency-switcher').not('.woocs_clean_select').val(data.current_currency);
                                    woocs_init_ddslick_select();
                                } catch (e) {
                                    console.log(e);
                                }
                            }
                            //***
                            if (woocs_drop_down_view == 'wselect' && woocs_is_mobile != 1) {
                                //https://github.com/websanova/wSelect
                                try {
                                    jQuery('select.woocommerce-currency-switcher').not('.woocs_clean_select').val(data.current_currency).trigger("change");
                                } catch (e) {
                                    console.log(e);
                                }
                            }
                            //***
                            /* auto switcher*/

                            var auto_switcher = jQuery('.woocs_auto_switcher');
                            if (auto_switcher.length > 0) {
                                woocs_auto_switcher_redraw(data.current_currency, auto_switcher);
                            }
                            woocs_sumbit_currency_changing = true;

                            var shortcode_currency = jQuery('strong.woocs_current_currency_code');
                            if (shortcode_currency.length) {
                                shortcode_currency.text(data.current_currency);
                            }


                            //***
                            //for another woocs switchers styles
                            document.dispatchEvent(new CustomEvent('after_woocs_get_products_price_html', {detail: {
                                    current_currency: data.current_currency
                                }}));
                        }

                    });

                } else {
                    woocs_sumbit_currency_changing = true;
                    
                }
		jQuery( ".single_variation_wrap" ).on( "show_variation", function ( event, variation ) {
			jQuery('.woocs_price_code').removeClass('woocs_preloader_ajax');
		} );			
		jQuery('.woocs_price_code').removeClass('woocs_preloader_ajax');
            }, 300);

        }
    }

    //***
    //removing price info on single page near variation prices
    setTimeout(function () {
        //jQuery('body.single-product .woocommerce-variation-price').find('.woocs_price_info').remove();
    }, 300);
    //***


});


function woocs_redirect(currency) {
    if (!woocs_sumbit_currency_changing) {
        return;
    }

    //***
    var l = window.location.href;
    //  l = l.replace('#', '');
    var l_tmp = l.split('#');
    l = l_tmp[0];
    var id_key = "";
    if (l_tmp.length > 1) {
        id_key = "#" + l_tmp[1]
    }
    //for #id navigation     l = l.replace(/(#.+$)/gi, '');

    l = l.split('?');
    l = l[0];
    var string_of_get = '?';
    woocs_array_of_get.currency = currency;

    /*
     l = l.replace(/(\?currency=[a-zA-Z]+)/g, '?');
     l = l.replace(/(&currency=[a-zA-Z]+)/g, '');
     */

    if (woocs_special_ajax_mode) {
        string_of_get = "";

        var data = {
            action: "woocs_set_currency_ajax",
            currency: currency
        };

        jQuery.post(woocs_ajaxurl, data, function (value) {
            location.reload();
        });

    } else {
        if (Object.keys(woocs_array_of_get).length > 0) {
            jQuery.each(woocs_array_of_get, function (index, value) {
                string_of_get = string_of_get + "&" + index + "=" + value;
            });

        }
        window.location = l + string_of_get + id_key;
    }


}

function woocs_refresh_mini_cart(delay) {
    /** Cart Handling */
    setTimeout(function () {
        try {

            //for refreshing mini cart
            $fragment_refresh = {
                url: wc_cart_fragments_params.ajax_url,
                type: 'POST',
                data: {action: 'woocommerce_get_refreshed_fragments', woocs_woocommerce_before_mini_cart: 'mini_cart_refreshing'},
                success: function (data) {
                    if (data && data.fragments) {

                        jQuery.each(data.fragments, function (key, value) {
                            jQuery(key).replaceWith(value);
                        });

                        try {
                            if ($supports_html5_storage) {
                                sessionStorage.setItem(wc_cart_fragments_params.fragment_name, JSON.stringify(data.fragments));
                                sessionStorage.setItem('wc_cart_hash', data.cart_hash);
                            }
                        } catch (e) {

                        }

                        jQuery('body').trigger('wc_fragments_refreshed');
                    }
                }
            };

            jQuery.ajax($fragment_refresh);


            /* Cart hiding */
            try {
                if (woocs_get_cookie('woocommerce_items_in_cart') > 0)
                {
                    jQuery('.hide_cart_widget_if_empty').closest('.widget_shopping_cart').show();
                } else {
                    jQuery('.hide_cart_widget_if_empty').closest('.widget_shopping_cart').hide();
                }
            } catch (e) {
                //***
            }


            jQuery('body').on('adding_to_cart', function () {
                jQuery('.hide_cart_widget_if_empty').closest('.widget_shopping_cart').show();
            });

        } catch (e) {
            //***
        }

    }, delay);

}

function woocs_get_cookie(name) {
    var matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
            ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

/*auto switcher*/

jQuery(function () {
    jQuery('.woocs_auto_switcher_link').on('click', function () {
        woocs_redirect(jQuery(this).data('currency'));
        return false;
    });

    jQuery('.woocs_auto_switcher li').on('click', function (e) {
        woocs_redirect(jQuery(this).find('a.woocs_auto_switcher_link').data('currency'));
        return false;
    });
});

function woocs_auto_switcher_redraw(curr_curr, switcher) {
    var view = switcher.data('view');
    switch (view) {
        case 'classic_blocks':
            switcher.find('a').removeClass('woocs_curr_curr');
            switcher.find('a[data-currency="' + curr_curr + '"]').addClass('woocs_curr_curr');
            break;
        case 'roll_blocks':
            switcher.find('a').removeClass('woocs_curr_curr');
            switcher.find('li').removeClass('woocs_auto_bg_woocs_curr_curr');
            var current_link = switcher.find('a[data-currency="' + curr_curr + '"]');
            current_link.addClass('woocs_curr_curr');
            current_link.parents('li').addClass('woocs_auto_bg_woocs_curr_curr');
            break;
        case 'round_select':
            switcher.find('a').removeClass('woocs_curr_curr');
            var current_link = switcher.find('a[data-currency="' + curr_curr + '"]');
            current_link.addClass('woocs_curr_curr');
            jQuery('.woocs_current_text').html(current_link.find('.woocs_base_text').html());
            break;
        default:
            break;
    }

}

function woocs_remove_link_param(key, sourceURL) {
    var rtn = sourceURL.split("?")[0],
            param,
            params_arr = [],
            queryString = (sourceURL.indexOf("?") !== -1) ? sourceURL.split("?")[1] : "";
    if (queryString !== "") {
        params_arr = queryString.split("&");
        for (var i = params_arr.length - 1; i >= 0; i -= 1) {
            param = params_arr[i].split("=")[0];
            if (param === key) {
                params_arr.splice(i, 1);
            }
        }
        rtn = rtn + "?" + params_arr.join("&");
    }
    return rtn;
}

function woocs_init_ddslick_select() {
    jQuery.each(jQuery('select.woocommerce-currency-switcher').not('.woocs_clean_select'), function (index, obj) {
        var width = jQuery(obj).data('width');
        var flag_position = jQuery(obj).data('flag-position');
        jQuery(obj).ddslick({
            //data: ddData,
            width: width,
            imagePosition: flag_position,
            selectText: "Select currency",
            //background:'#ff0000',
            onSelected: function (data) {
                if (!woocs_loading_first_time)
                {
                    var form = jQuery(data.selectedItem).closest('form.woocommerce-currency-switcher-form');
                    jQuery(form).find('input[name="woocommerce-currency-switcher"]').eq(0).val(data.selectedData.value);

                    if (Object.keys(woocs_array_of_get).length == 0) {
                        //jQuery(form).submit();
                        woocs_redirect(data.selectedData.value);
                    } else {
                        woocs_redirect(data.selectedData.value);
                    }
                }

            }
        });
    });
}

