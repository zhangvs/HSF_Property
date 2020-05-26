var page = 1;
var isNoData = false;
var heightArr = [0,0];
var sidx = GetQueryString("sidx") == null ? "Id" : GetQueryString("sidx");
var groupId = GetQueryString("groupId") == null ? 0 : GetQueryString("groupId");

var windowWidth = $(this).width() > 640 ? 640 : $(this).width();//浏览器视口的宽度
var boxWidth = windowWidth/2;



//加载第一页10条数据
getPageData();

window.onload = function () {
    //init();
    initItem();
    //$(window).on('resize', function () {
    //    init();
    //})
}

$(window).scroll(function () {
    window.location.href = "/WeChatManage/Sikelai/ListsNew?sidx=Id";
});



function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}



//function init() {
    //var winBox = $(window).width() > 640 ? 640 : $(window).width();
    //var boxWidth = $(".skl_box_item").outerWidth(true);
    //var parseInt(winBox / boxWidth)
    //var cols = 2;
    //var heightArr = [];
    //for (var i = 0; i < cols; i++) {
    //    heightArr.push(0);
    //}

//}
function initItem() {
    $(".skl_box_item.noPosition").each(function (index, item) {
        var idx = 0;
        var minBoxHeight = heightArr[0];
        for (var i = 0; i < heightArr.length; i++) {
            if (heightArr[i] < minBoxHeight) {
                minBoxHeight = heightArr[i];
                idx = i;
            }
        }
        $(item).css({
            left: boxWidth * idx,
            top: minBoxHeight
        });
        $(item).removeClass("noPosition");
        heightArr[idx] += $(this).outerHeight(true);
        $(".skl_box").css({
            height: heightArr[idx]+100,
        });
    })
}

function getPageData() {
    var index = layer.load(0, { shade: false });
    $.ajax({
        url: "/WeChatManage/Sikelai/GetPageData",
        type: "POST",
        data: { page: page, sidx: sidx, groupId: groupId },
        async: false,
        success: function (listStr) {
            layer.close(index);
            var list = JSON.parse(listStr);
            if (list.length<10) {
                isNoData = true;
            }
            if (list.length>0) {
                for (var item in list) {
                    imgReady(list[item].Pic1, function () {
                    });
                    $(".skl_box").append(
                        '<div class="skl_box_item noPosition">' +
                        '<a href="/WeChatManage/Sikelai/Details?id=' + list[item].Id + '">' +
                        '<img src="' + list[item].Pic1 + '" />' +
                        '<div class="skl_box_item_text">' +
                        '<span class="skl_box_item_text_No">' + list[item].Id + ' 号</span>' +
                        '<span class="skl_box_item_text_Name">' + list[item].FullName + '</span>' +
                        '</div>' +
                        '</a>' +
                        '<p class="skl_box_item_Tou">' +
                        '<span>' + list[item].PollCount + ' 票</span>' +
                        '<span><a href="/WeChatManage/Sikelai/PollSave?id=' + list[item].Id + '&name=' + list[item].FullName + '"><img src="/Content/images/icon/心型填充.png" />投票</a></span>' +
                        '</p>' +
                        '</div>'
                    )
                }
            }

        },
        error: function () {

        }
    });
}
