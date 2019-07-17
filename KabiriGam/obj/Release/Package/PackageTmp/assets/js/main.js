/*
 *****************************************************
 *	CUSTOM JS DOCUMENT                              *
 *	Single window load event                        *
 *   "use strict" mode on                            *
 *****************************************************
 */
$(window).on("load", function() {

    "use strict";

    var preLoader = $('.preloader');
    var countNumber = $(".count-number");
    var faqsAccordion = $('#faqs-accordion');
    var fancybox = $('.fancybox');
    var linksListsItem = $('.links-lists li');
    var MixItUp1 = $('#MixItUp1');
    var priceRange = $("#slider-range");
    var amnt = $("#amount");

    // ============================================
    // PreLoader On window Load
    // =============================================

    preLoader.addClass('loaderout');

    // ============================================
    // Fun Factor / Counter
    // =============================================	

    countNumber.appear(function() {
        $(this).each(function() {
            var datacount = $(this).attr('data-count');
            $(this).find('.counter').delay(6000).countTo({
                from: 10,
                to: datacount,
                speed: 3000,
                refreshInterval: 50,
            });
        });
    });



    //========================================
    // Accordion 
    //======================================== 	

    if (faqsAccordion.length) {
        faqsAccordion.accordion();
    }

    //========================================
    // LightBox / Fancybox
    //======================================== 	

    if (fancybox.length) {
        fancybox.fancybox();
    }
    //============================================
    // MixItUp settings
    //============================================

    if (MixItUp1.length) {
        MixItUp1.mixItUp({
            selectors: {
                filter: '.filter-1'
            }
        });
    }
    //========================================
    // List Toggle 
    //======================================== 	

    //========================================
    // List Toggle 
    //======================================== 	

    linksListsItem.on('click', function(e) {
        var linksListsSubList = $('.sub-list.active');
        if ($(this).find('>ul').hasClass('active')) {

            $(this).children('ul').removeClass('active').children('li').slideUp();
            linksListsItem.parent('ul').children('li').removeClass('active');
            linksListsSubList.parent('li').addClass('active');
            $(this).addClass('active');
            if ($(this).hasClass('collapse-link')) {
                $(this).children('a').children('i').removeClass('fa-angle-down');
                $(this).children('a').children('i').addClass('fa-angle-right');
                e.preventDefault();
            }
            if ($(this).hasClass('active')) {
                $(this).removeClass('active');
            }

            e.stopPropagation();
        } else {

            $(this).children('ul').addClass('active').children('li').slideDown();

            linksListsItem.parent('ul').children('li').removeClass('active');
            linksListsSubList.parent('li').addClass('active');
            $(this).addClass('active');
            if ($(this).hasClass('collapse-link')) {
                $(this).children('a').children('i').removeClass('fa-angle-right');
                $(this).children('a').children('i').addClass('fa-angle-down');
            }
            e.stopPropagation();
        }
    });


    //***************************************
    // Map initialization function Calling
    //****************************************

    initMap();

    //========================================
    //	Price Rannge Filter Settings
    //======================================== 

    if (priceRange.length) {
        priceRange.slider({
            range: true,
            min: 5000,
            max: 1000000,
            values: [150000, 600000],
            slide: function(event, ui) {
                amnt.val(" " + ui.values[0] + " - " + ui.values[1]);
            }
        });

        amnt.val(" " + priceRange.slider("values", 0) +
            " - " + priceRange.slider("values", 1));
    }

    //========================================
    // Checkout Page Effect function Calling
    //======================================== 	

    checkoutPageEffect();

    //***************************************
    // All Owl Carousel function Calling
    //****************************************

    owlCarouselInit();


}); // End of the window load event


//========================================
// Checkout Page Effect function
//======================================== 	

function checkoutPageEffect() {
    "use strict";

    var showlogin = $('.showlogin');
    var loginDiv = $('.login');
    var showcoupon = $('.showcoupon');
    var checkout_coupon = $('.checkout_coupon');
    var differentAddress = $('#ship-to-different-address-checkbox');
    var shippingFields = $('.shipping-fields');
    var createAccountCheck = $('#createaccount');
    var createAccount = $('.create-account');
    var paymentMethodCheque = $('#payment_method_cheque');
    var paymentBox = $('.payment_box.payment_method_cheque');
    var paymentMethodPaypal = $('#payment_method_paypal');
    var paymentBoxPaypal = $('.payment_box.payment_method_paypal');


    showlogin.on('click', function(e) {
        e.preventDefault();
        loginDiv.slideToggle("slow");
    });

    showcoupon.on('click', function(e) {
        e.preventDefault();
        checkout_coupon.slideToggle("slow");
    });

    differentAddress.change(function() {
        if (this.checked) {
            shippingFields.slideToggle('slow');
        } else {
            shippingFields.slideToggle('slow');
        }
    });

    createAccountCheck.change(function() {
        if (this.checked) {
            createAccount.slideToggle('slow');
        } else {
            createAccount.slideToggle('slow');
        }
    });


}
//***************************************
// Contact Page Map
//****************************************  

function initMap() {
    "use strict";

    var mapDiv = $('#gmap_canvas');

    if (mapDiv.length) {
        var myOptions = {
            zoom: 16,
            scrollwheel: false,
            draggable: true,
            center: new google.maps.LatLng(36.5688975, 53.0659018),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions);
        var marker = new google.maps.Marker({
            map: map,
            position: new google.maps.LatLng(36.5688975, 53.0659018)
        });
        var infowindow = new google.maps.InfoWindow({
            content: '<strong>درنیکا</strong><br>البرز افراز طبرستان<br>'
        });
        google.maps.event.addListener(marker, 'click', function() {
            infowindow.open(map, marker);
        });

        infowindow.open(map, marker);
    }

}


//***************************************
// All owl Carousels 
//****************************************   

function owlCarouselInit() {

    "use strict";

    //==========================================
    // owl carousels settings
    //===========================================

    var home1MainSlider = $('#home1-main-slider');
    var testimonialSection2 = $('#testimonial-section2');
    var waPartnerCarousel = $('.wa-partner-carousel');
    var shopCarousel = $("#shop-carousel");
    var comingSoonSilder = $('#coming-soon-slider');

    if (home1MainSlider.length) {
        home1MainSlider.owlCarousel({
            autoPlay: true,
            items: 1,
            singleItem: true,
            navigation: true,
            pagination: true,

        });
    }

    if (testimonialSection2.length) {
        testimonialSection2.owlCarousel({
            autoPlay: false,
            items: 3,
            navigation: true,
            pagination: false,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3]

        });
    }

    if (shopCarousel.length) {
        shopCarousel.owlCarousel({
            autoPlay: false,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }

    if (waPartnerCarousel.length) {
        waPartnerCarousel.owlCarousel({
            autoPlay: true,
            items: 6,
            itemsDesktop: [1199, 5],
            itemsDesktopSmall: [979, 4],
            itemsTablet: [768, 3],
            itemsMobile: [767, 2],
            margin: 0,
            navigation: false,
            pagination: false

        });
    }
    /*** Bootstrap Carousel ***/
    if (comingSoonSilder.length) {
        comingSoonSilder.carousel({
            interval: 3000
        });
        var comingSoonBgArr = ["assets/img/backgrounds/coming-soon-bg-2.jpg",
            "assets/img/backgrounds/coming-soon-bg.jpg",
            "assets/img/backgrounds/coming-soon-bg-3.jpg"
        ];

        var csItem = comingSoonSilder.children('.carousel-inner').children('.item');
        var csItemLength = csItem.length;
        var csCount;

        for (csCount = 0; csCount < csItemLength; csCount++) {
            if (csCount === 0) {
                comingSoonSilder.children('.carousel-inner').children('.item:first-child').children('.fill').attr('style', 'background-image:url("' + comingSoonBgArr[csCount] + '")');
            } else if (csCount === csItemLength - 1) {
                comingSoonSilder.children('.carousel-inner').children('.item:last-child').children('.fill').attr('style', 'background-image:url("' + comingSoonBgArr[csCount] + '")');
            } else {
                comingSoonSilder.children('.carousel-inner').children('.item:nth-child(' + (csCount + 1) + ')').children('.fill').attr('style', 'background-image:url("' + comingSoonBgArr[csCount] + '")');
            }
        }

    }

}

/*
*****************************************************
*	END OF THE JS 									*
*	DOCUMENT                       					*
*****************************************************
*/