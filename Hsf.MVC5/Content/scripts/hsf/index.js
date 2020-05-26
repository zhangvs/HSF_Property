
//倒计时
countDown('2019-08-15');

//带天数的倒计时
function countDown(completeTime) {
    var stime = Date.parse(new Date());
    var etime = Date.parse(new Date(completeTime));
    var times = (etime - stime) / 1000;  //两个时间戳相差的毫秒数
    var timer = null;
    timer = setInterval(function () {
        var day = 0,
            hour = 0,
            minute = 0,
            second = 0;//时间默认值
        if (times > 0) {
            day = Math.floor(times / (60 * 60 * 24));
            hour = Math.floor(times / (60 * 60)) - (day * 24);
            minute = Math.floor(times / 60) - (day * 24 * 60) - (hour * 60);
            second = Math.floor(times) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
        }
        if (day <= 9) day = '0' + day;
        if (hour <= 9) hour = '0' + hour;
        if (minute <= 9) minute = '0' + minute;
        if (second <= 9) second = '0' + second;
        // 15 天 23 小时 12 分 33 秒
        $(".djs").html(day + " 天 " + hour + " 小时 " + minute + " 分 " + second + " 秒")
        //console.log(day + "天:" + hour + "小时：" + minute + "分钟：" + second + "秒");
        times--;
    }, 1000);
    if (times <= 0) {
        clearInterval(timer);
    }
}



//页面层
//$(function () {
//    $(".skl_box_item_text_Max").click(
//        function () {
//            layer.open({
//                title: false,
//                type: 2,
//                shadeClose: true,
//                shade: false,
//                maxmin: true, //开启最大化最小化按钮
//                skin: 'layui-layer-rim', //加上边框
//                area: ['90%', '90%'], //宽高
//                content: '/WeChatManage/Sikelai/Details'
//            });
//        }
//    )
//});

//$(".skl_sort").click(
//    function () {
//        var liParent = $(this).parent().parent();
//        var brother = liParent.children().children();
//        brother.removeClass("skl_sort_now");
//        $(this).addClass("skl_sort_now");
//    }
//);