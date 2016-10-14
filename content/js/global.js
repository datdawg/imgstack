$(document).ready(function () {
    console.log('det virker!');

    if ($('.slide-holder').length > 0) {
        $('main').addClass('homepage');
    }

    //fix slick-overflow glitch on pageload
    $('.slick-inner').on('init', function (event, slick) {
        $(this).css('opacity', '1');
    });

    $('.slick-inner').slick({
        accessibility: true,
        infinite: true,
        arrows: true,
        swipe: false,
        speed: 500,
        slidesToShow: 7,
        centerMode: true,
        autoplay: true,
        cssEase: 'ease-out',
        prevArrow: '<div class="slick-prev"><i class="fa fa-chevron-left"></i></div>',
        nextArrow: '<div class="slick-next"><i class="fa fa-chevron-right"></i></div>',
        variableWidth: true
    });

    $('.user-options-toggle').on('click', function () {
        $('.user-image').toggleClass('spin');
        $('.user-options').toggleClass('open');
    });

});