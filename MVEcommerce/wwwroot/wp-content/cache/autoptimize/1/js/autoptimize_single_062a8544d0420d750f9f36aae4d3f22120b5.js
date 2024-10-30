/*!
 * jQuery serializeObject - v0.2-wp - 1/20/2010
 * http://benalman.com/projects/jquery-misc-plugins/
 *
 * Copyright (c) 2010 "Cowboy" Ben Alman
 * Dual licensed under the MIT and GPL licenses.
 * http://benalman.com/about/license/
 */
(function($,undefined){'$:nomunge';$.fn.serializeObject=function(){var obj={};$.each(this.serializeArray(),function(i,o){var n=o.name,v=o.value;obj[n]=obj[n]===undefined?v:Array.isArray(obj[n])?obj[n].concat(v):[obj[n],v];});return obj;};})(jQuery);