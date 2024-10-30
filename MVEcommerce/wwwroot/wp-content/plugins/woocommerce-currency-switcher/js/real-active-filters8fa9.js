let woocs_interval_active_filter;
let woocs_interval_active_filter_count = 0;	 
woocs_interval_active_filter = setInterval(woocs_search_active_filter, 333);

function woocs_search_active_filter () {

    let active_filters = jQuery('.wc-blocks-filter-wrapper .wc-block-active-filters__list-item-name');

    
    if (woocs_current_currency.name == woocs_default_currency.name || woocs_interval_active_filter_count > 15) {
	clearInterval(woocs_interval_active_filter );
	woocs_interval_active_filter  = null;	
	return;
    } 
    
    const params = new Proxy(new URLSearchParams(window.location.search), {
      get: (searchParams, prop) => searchParams.get(prop),
    });       
    
    if (jQuery(active_filters).length && (params.min_price || params.max_price )) {

	const unit = window.wc.priceFormat.getCurrency();

	jQuery( active_filters ).each(function( index ) {
	  if (params.min_price) {
	     const converted_min = window.wc.priceFormat.formatPrice(woocs_convert_price_filter(params.min_price)* Math.pow(10,unit.minorUnit));
	    jQuery(this).html(function(index, html){
		   return html.replaceAll(window.wc.priceFormat.formatPrice(params.min_price*Math.pow(10,unit.minorUnit)), converted_min);
	    });	      
	  }
	  if (params.max_price) {
	     const converted_max = window.wc.priceFormat.formatPrice(woocs_convert_price_filter(params.max_price)* Math.pow(10,unit.minorUnit));
	    jQuery(this).html(function(index, html){
		   return html.replaceAll(window.wc.priceFormat.formatPrice(params.max_price*Math.pow(10,unit.minorUnit)), converted_max);
	    });	      
	  }
	  
	});
	
	clearInterval(woocs_interval_active_filter );
	woocs_interval_active_filter  = null;
    }   
    woocs_interval_active_filter_count++;
}

