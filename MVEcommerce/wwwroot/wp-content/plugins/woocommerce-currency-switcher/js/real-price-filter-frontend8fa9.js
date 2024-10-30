let woocs_interval_search;
	 
woocs_interval_search = setInterval(woocs_search_oprice_filter, 333);

function woocs_search_oprice_filter () {

    let price_filter = jQuery('div[data-filter-type="price-filter"] .wc-block-price-slider input');

    if (jQuery(price_filter).length) {
	if (woocs_current_currency.name == woocs_default_currency.name) {
	    clearInterval(woocs_interval_search );
	    woocs_interval_search  = null;	
	    return;
	}	
	
	let  price_filter_clon = jQuery(price_filter).parents('.wc-block-price-filter__controls').clone();
	jQuery(price_filter).parents('.wc-block-price-filter__controls').replaceWith(price_filter_clon )
	
	

	clearInterval(woocs_interval_search );
	woocs_interval_search  = null;	
	woocs_init_real_price_filter();
    }
    
    
}

function woocs_init_real_price_filter() {
    
    let cuerrency_data = window.wc.priceFormat.getCurrency();
   
    let minorUnit = 10 ** cuerrency_data.minorUnit;

    let price_filter_wraper =  jQuery('div[data-filter-type="price-filter"]');
    
    let min_input = jQuery(price_filter_wraper).find('.wc-block-price-filter__range-input--min');
    let max_input = jQuery(price_filter_wraper).find('.wc-block-price-filter__range-input--max');
    
    
    let min_current = jQuery(min_input).val();
    //jQuery(price_filter_wraper).find('input.wc-block-price-filter__amount--min').attr('value',woocs_convert_price_slider( min_current/100) )
    jQuery(price_filter_wraper).find('input.wc-block-price-filter__amount--min').val(window.wc.priceFormat.formatPrice( woocs_convert_price_filter(min_current)));    
    
    let max_current = jQuery(max_input).val();
    //jQuery(price_filter_wraper).find('input.wc-block-price-filter__amount--max').attr('value',woocs_convert_price_slider( max_current/100))
    jQuery(price_filter_wraper).find('input.wc-block-price-filter__amount--max').val(window.wc.priceFormat.formatPrice( woocs_convert_price_filter(max_current)));
    
    
    
    jQuery(min_input).on('input', function(){
	let min = jQuery(this).val();
	jQuery('input.wc-block-price-filter__amount--min').val(window.wc.priceFormat.formatPrice(woocs_convert_price_filter(min)));
	
    });
    jQuery(max_input).on('input', function(){
	let max = jQuery(this).val();
	jQuery('input.wc-block-price-filter__amount--max').val(window.wc.priceFormat.formatPrice(woocs_convert_price_filter(max)));
	
    }); 
    
    jQuery('input.wc-block-price-filter__amount--min').on('focus',function(){
	let min = jQuery(min_input).val();
	jQuery(this).attr('type', 'number');
	return jQuery(this).val(woocs_convert_price_filter(min)/minorUnit);
    });
     jQuery('input.wc-block-price-filter__amount--max').on('focus',function(){
	let max = jQuery(max_input).val();
	jQuery(this).attr('type', 'number');
	return jQuery(this).val(woocs_convert_price_filter(max)/minorUnit);
    });  
    
    
     jQuery('input.wc-block-price-filter__amount--min').on('focusout',function(){
	let min = jQuery(this).val() * minorUnit;
	let old_min = jQuery(min_input).val();
	let range_min = jQuery(min_input).attr('min');
	let range_max = jQuery(min_input).attr('max');
	jQuery(this).attr('type', 'text');	
	if (woocs_convert_price_filter(old_min) == min ) {
	    jQuery(this).attr('value', window.wc.priceFormat.formatPrice(woocs_convert_price_filter(old_min)));
	    jQuery(this).val(window.wc.priceFormat.formatPrice(woocs_convert_price_filter(old_min)));
	    return false;
	}	
	
	jQuery(min_input).trigger('focus');

	jQuery(min_input).attr('value', woocs_back_convert_price_filter(min))
	jQuery(min_input).val(woocs_back_convert_price_filter(min));
	
	jQuery(this).attr('value', window.wc.priceFormat.formatPrice(min));

	
	let percent = parseInt(((jQuery(min_input).val() - range_min)/(range_max - range_min))*100);
	jQuery('.wc-block-price-filter__range-input-progress').css("--low", percent + "%");
	   
	 jQuery(this).val(window.wc.priceFormat.formatPrice(min));
	 
	 let url_val = {}
	 url_val['min_price'] = -1;
	 if ( jQuery(min_input).val() != range_max) {
	     url_val['min_price'] = jQuery(min_input).val()/minorUnit ;
	 }


	 if (parseInt(jQuery(max_input).val()) < parseInt(jQuery(min_input).val())) {
	     url_val['max_price'] = -1;

	 }
	 
	 woocs_do_price_filter(url_val);
	 return true;	 
     });
     jQuery('input.wc-block-price-filter__amount--max').on('focusout',function(){
	let max = jQuery(this).val() * minorUnit;
	let old_max = jQuery(max_input).val();
	let range_min = jQuery(max_input).attr('min');
	let range_max = jQuery(max_input).attr('max');	
	jQuery(this).attr('type', 'text');
	if (woocs_convert_price_filter(old_max) == max ) {
	    jQuery(this).attr('value', window.wc.priceFormat.formatPrice(woocs_convert_price_filter(old_max)));
	    jQuery(this).val(window.wc.priceFormat.formatPrice(woocs_convert_price_filter(old_max)));
	    return false;
	}
	
	
	jQuery(max_input).trigger('focus');
	
	jQuery(max_input).attr('value', woocs_back_convert_price_filter(max))
	jQuery(max_input).val(woocs_back_convert_price_filter(max))
	jQuery(this).attr('value', window.wc.priceFormat.formatPrice(woocs_convert_price_filter(max)))
	
	let percent = parseInt(((jQuery(max_input).val() - range_min)/(range_max - range_min))*100);
	jQuery('.wc-block-price-filter__range-input-progress').css("--high", percent + "%");	
	jQuery(this).val(window.wc.priceFormat.formatPrice(max));
	
	 let url_val = {}
	 url_val['max_price'] = -1;
	 if ( jQuery(max_input).val() != range_max) {
	     url_val['max_price'] = jQuery(max_input).val()/minorUnit ;
	 }


	 if (parseInt(jQuery(max_input).val()) < parseInt(jQuery(min_input).val())) {
	     url_val['min_price'] = -1;

	 }
	 
	 woocs_do_price_filter(url_val);
	 return true;
     });     
}
function  woocs_do_price_filter(properties) {

   var url = new URL(window.location.href);
   

   Object.keys(properties).forEach(key => {     
	if (properties[key] == -1) {
	   url.searchParams.delete(key);
       }else {
	   url.searchParams.set(key, properties[key]);
       }      
  });
   window.location.href = url.href;

}
function  woocs_unformat_price_filter(formated_price) {
    let price = 0;
    let curr_price = window.wc.priceFormat.getCurrency();
 
    formated_price = formated_price.replace(curr_price.symbol,'').replace(' ','').replace(curr_price.thousandSeparator,'').replace(curr_price.decimalSeparator,'.');

    
    return  price;
}
function  woocs_back_convert_price_filter(price) {
    var label = price;
    

    if (woocs_current_currency.rate !== 1) {
        label = parseInt(label / parseFloat(woocs_current_currency.rate));
    }

    //+++
    return label;
}
function  woocs_convert_price_filter(price) {
    var label = price;
    if (woocs_current_currency.rate !== 1) {
        label = parseInt(label * parseFloat(woocs_current_currency.rate));
    }

    //+++
    return label;
}