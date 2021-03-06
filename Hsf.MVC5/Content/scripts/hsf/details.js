﻿window.onload = function () {
    banner();
}

var banner = function () {
    var banner = document.querySelector('.jd_banner');
    var imageBox = banner.querySelector('ul:first-child');//此dom不能检测move事件
    var pointBox = banner.querySelector('ul:last-child');
    var points = pointBox.querySelectorAll('li');

    var imgs = imageBox.querySelectorAll('li img');
    for (var i = 0; i < imgs.length; i++) {
        if (imgs[i].src == "") {
            imgs[i].parentElement.remove();
            points[i].remove();
        }
    }
    var imgLis = imageBox.querySelectorAll('li');
    var index = 0;
    var widths = imgLis.length;
    imageBox.style.width = widths + '00%';
    var liwidth = 100 / widths;
    for (var i = 0; i < widths; i++) {
        imgLis[i].style.width = liwidth + '%';
    }
    var banner1 = document.querySelector('.jd_banner');
    var width = banner1.clientWidth;

    //设置过渡
    var addTransition = function () {
        imageBox.style.transition = 'all 0.5s';
        imageBox.style.webkitTransition = 'all 0.5s';//不能与定时器时间一致
    }
    //清过渡
    var removeTransition = function () {
        imageBox.style.transition = 'none';
        imageBox.style.webkitTransition = 'none';
    }
    //位移
    var setTranslateX = function (translateX) {
        imageBox.style.transform = 'translateX(' + translateX + 'px)';
        imageBox.style.webkitTransform = 'translateX(' + translateX + 'px)';
    }

    ////d定时器
    //var timer = setInterval(function () {
    //    index++;
    //    addTransition();
    //    setTranslateX(-index * width);
    //}, 5000);
    ////监听过渡结束
    //imageBox.addEventListener("transitionend", function () {
    //    if (index >= 5) {
    //        index = 1;
    //        removeTransition();
    //        setTranslateX(-index * width);
    //    }
    //    else if (index <= 0) {
    //        index = 4;
    //        removeTransition();
    //        setTranslateX(-index * width);
    //    }
    //    setPoint();
    //})

    var setPoint = function () {
        for (var i = 0; i < points.length; i++) {
            points[i].classList.remove('now');
        }
        if (index< points.length && index>=0) {
            points[index].classList.add('now');
        }
        
    }

    var startX = 0;
    var distanceX = 0;
    var isMove = false;


    banner.addEventListener("touchstart", function (e) {
        //clearInterval(timer);
        startX = e.touches[0].clientX;
    });
    //滑动事件，
    banner.addEventListener("touchmove", function (e) {
        var moveX = e.touches[0].clientX;
        //console.log(moveX);
        distanceX = moveX - startX;
        var translateX = -index * width + distanceX;
        //console.log(distanceX);
        removeTransition();
        setTranslateX(translateX);
        isMove = true;
    });

    banner.addEventListener("touchend", function (e) {
        if (isMove) {
            if (Math.abs(distanceX) < width / 3) {
                addTransition();
                setTranslateX(-index * width);
            }
            else
            {
                if (distanceX > 0) {
                    index--;
                }
                else {
                    index++;
                }
                //if (index >= widths) {
                //    index = 0;
                //}
                //else if (index < 0) {
                //    index = widths-1;
                //}
                addTransition();
                setTranslateX(-index * width);
                setPoint();
            }
        }

        //clearInterval(timer);
        //timer = setInterval(function () {
        //    index++;
        //    addTransition();
        //    imageBox.style.transform = 'translateX(' + (-index * width) + 'px)';
        //    imageBox.style.webkitTransform = 'translateX(' + (-index * width) + 'px)';
        //}, 5000);
        //startX = 0;
        //distanceX = 0;
        //isMove = false;
    });


}

