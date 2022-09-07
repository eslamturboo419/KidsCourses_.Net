    /*==============================================
                                /* ========= Avoid 'console' errors ============
                                /*==============================================*/

    var method;

    var noop = function() {};

    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeline', 'timelineEnd', 'timeStamp', 'trace', 'warn'
    ];

    var length = methods.length;

    var console = (window.console = window.console || {});

    while (length--) {

        method = methods[length];

        // Only stub undefined methods.

        if (!console[method]) {

            console[method] = noop;
        }
    }



    /*=====================================
    /*================ Wow ===============
    /*===================================== */

    if ($(window).width() > 767) {
        var wow = new WOW({
            boxClass: 'wow',
            animateClass: 'animated',
            offset: 0,
            mobile: false,
            live: true
        });
        new WOW().init();
    }


    /*=====================================
    /*============= Owl Carousel ==========
    /*===================================== */


    /*======= Teachers =========== */
    function teachers() {

        $("#owl-teachers").owlCarousel({
            pagination: false,
            slideSpeed: 800,
            paginationSpeed: 800,
            smartSpeed: 500,
            autoplay: true,
            singleItem: true,
            loop: true,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                580: {
                    items: 1
                },
                900: {
                    items: 1
                }
            }
        });
    };

    /*======= Popular Teachers =========== */
    function popularTeachers() {

        $("#popular-teachers").owlCarousel({
            pagination: false,
            slideSpeed: 800,
            paginationSpeed: 800,
            smartSpeed: 500,
            autoplay: true,
            singleItem: true,
            loop: true,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                580: {
                    items: 2
                },
                900: {
                    items: 4
                }
            }
        });
    };
    /*---------- student-course-tab ----------*/
    function student_course_tab(){

        $("#student-course-tab").owlCarousel({
            pagination: false,
            slideSpeed: 800,
            paginationSpeed: 800,
            smartSpeed: 500,
            autoplay: true,
            singleItem: true,
            loop: true,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                580: {
                    items: 2
                },
                900: {
                    items: 3
                }
            }
        });
    }
    /*===================================
/* { 01 } => Home Slider Carousel
/*===================================*/
    function home_slider() {
        $('#home-slider').owlCarousel({
            loop: true,
            autoplay: true,
            margin: 0,
            animateOut: 'fadeOut',
            animateIn: 'fadeIn',
            nav: false,
            slideSpeed: 800,
            smartSpeed: 500,
            dots: true,
            items: 1,
            responsive: {
                0: {
                    items: 1,
                },
                500: {
                    items: 1,
                },
                900: {
                    items: 1,
                }
            }
        });
    }

    /*=====================================
    /*============= FancyBox ==============
    /*===================================== */

    function fancybox() {

        $('[data-fancybox]').fancybox({
            youtube: {
                controls: 0,
                showinfo: 0
            },

            protect: true,

            vimeo: {
                color: 'f00'
            },

            spinnerTpl: '<div class="fancybox-loading"></div>',
        });
    }

    /*=====================================
    /*============= NiceScroll ============
    /*===================================== */

    function nicescroll() {

        $("html").niceScroll({
            zindex: 13253252,
            cursorborder: 0,
            background: "white",
            cursorcolor: "#3b3e79",
            cursorwidth: "10px",
            border: 0,
            overflowX: 0,
            cursorborderradius: 0,
        });
    }

    function dropdown(){
        $('.dropdown-body').niceScroll({
            zindex: 13253252,
            cursorborder: "0px",
            background: "white",
            cursorcolor: "#45487d",
            cursorwidth: "10px",
            border: 0,
            overflowX: 0,
            cursorborderradius: "0px",
        });
    }

    /*=====================================
    /*============= Preloader ============
    /*===================================== */

    $(window).on('load', function() {

        $('.preloader#preloader').fadeOut(300);

    });

    /*=====================================
    /*============= MatchHeight ============
    /*===================================== */

    function matchHeight(){
        $('.item').matchHeight();
    }

    /*=====================================
    /*============= CounterUp ============
    /*===================================== */
    function counter(){
        $('.counter').counterUp({
            delay: 10,
            time: 1000,
            offset: 70,
        });
    };
    /*=====================================
    /*============= MixItUp ============
    /*===================================== */
    function mixItUp(){
        var mixer = mixitup($('#courses-wrapper'));
    }

    /*=====================================
    /*============= ButtonClick ==========
    /*===================================== */

    $('.search-button').on('click',function(){

        $('.menu-right .search-content div').toggleClass('show');

    });
    $('.navbar-header .menu-left .menu-left-dropdown').on('click',function () {

        $('.navbar-header .menu-left .column').toggleClass('show');

    });
    $('.site-enter .site-enter-button>i').on('click',function () {

        $('.site-enter-button .column').toggleClass('show');

    });
    $('.site-enter-button .column a i.search').on('click',function () {

        $('.site-enter-button .column a .form-search').toggleClass('show');

    });

    $('.menu-right .notification-site>.notification-display').on('click',function () {

        $('.menu-right .notification-site .notification-display1').toggleClass('show')

    });

    $('.menu-right .notification-site>.avater-display').on('click',function () {
        $('.menu-right .notification-site .avater-site').toggleClass('show');
    });

    $('.site-enter1 a:first-child').on('click',function () {
        $('.site-notification .column1').toggleClass('show');
    })

    $('.site-enter1 .avater-display1').on('click',function () {
        $('.site-notification .column2').toggleClass('show');
    })