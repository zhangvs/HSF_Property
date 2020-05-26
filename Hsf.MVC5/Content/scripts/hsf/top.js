var page = 1;
var isNoData = false;
var heightArr = [0,0];
var sort = 1;
var sidx = GetQueryString("sidx") == null ? "PollCount" : GetQueryString("sidx");
var groupId = GetQueryString("groupId") == null ? 0 : GetQueryString("groupId");


function GetQueryString(name) {

    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;

}

$(window).scroll(function () {
    var scrollTop = $(this).scrollTop();//滚动条在Y轴上的滚动距离
    var scrollHeight = $(document).height();//文档的总高度
    var windowHeight = $(this).height();//浏览器视口的高度
    //提前200px加载下一页
    if (scrollTop + windowHeight + 200 > scrollHeight) {
        if (!isNoData) {
            page++;
            getPageData(page);
        }
    }
});

//加载第一页10条数据
getPageData();



function getPageData() {
    var index = layer.load(0, { shade: false });
    $.ajax({
        url: "/WeChatManage/Sikelai/GetPageData",
        type: "POST",
        data: { page: page, sidx: sidx, groupId: groupId },
        async: false,
        success: function (listStr) {
            var list = JSON.parse(listStr);
            if (list.length<10) {
                isNoData = true;
            }
            layer.close(index);
            for (var item in list) {
                imgReady(list[item].Pic1, function () {
                });
                $(".skl_top_list ul").append(
                    '<li>' +
                    '    <a href="/WeChatManage/Sikelai/Details?id=' + list[item].Id + '">' +
                    '        <span>'+ sort++ +'</span>' +
                    '        <span>' +
                    '            <img src="' + list[item].Pic1 + '" />' +
                    '        </span>' +
                    '        <span>' + list[item].Id + '</span>' +
                    '        <span>' + list[item].FullName + '</span>' +
                    '        <span>' + list[item].PollCount + '</span>' +
                    '    </a>' +
                    '</li>'
                )
            }
        },
        error: function () {

        }
    });
}

