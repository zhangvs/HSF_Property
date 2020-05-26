layui.use('upload', function () {
    var $ = layui.jquery
        , upload = layui.upload;
    //拖拽上传
    upload.render({
        elem: ".upload"
        , url: '/WeChatManage/SiKeLai/AddWater'
        , done: function (data) {
            console.log(data);
            if (data.Success) {
                var path = data.Message;
                var maxheight = this.item[0].clientHeight;
                $("#" + this.item[0].id).html("<img src='" + path + "' style='max-height:" + maxheight+"px' /><i class='iconfont icon-icon-test' onclick='delUpload(this)'></i>").attr("datasrc", path).removeClass("upload");
                
            } else {
                alertMsg("上传失败:" + data.Message);
            }
        }
        , error: function () {
            alertMsg("上传异常!");
        }
    });
});

function delUpload(e) {
    $(e).parent().html("").removeAttr("datasrc").addClass("upload");
}

function valiCode() {
    var telphone = $.trim($("#Telphone").val());
    if (telphone == "" || telphone.length == 0) {
        alertMsg("手机号码不能为空!");
        return;
    }
    if (!valiMobileNumber(telphone)) {
        alertMsg("手机号码格式有误!");
        return;
    }

    var fullName = $.trim($("#FullName").val());
    if (fullName == "" || fullName.length == 0) {
        alertMsg("姓名不能为空!");
        return;
    } else if (fullName.length < 2) {
        alertMsg("姓名不能少于2个汉字!!");
        return;
    }


    var groupid = $("#GroupId").val();
    if (groupid == "" || groupid.length == 0) {
        alertMsg("请选择报名的赛组!");
        return;
    }

    var pic1 = $("#Pic1").attr("datasrc");
    var pic2 = $("#Pic2").attr("datasrc");
    var pic3 = $("#Pic3").attr("datasrc");
    var pic4 = $("#Pic4").attr("datasrc");
    if (typeof (pic1) == "undefined") {
        alertMsg("封面图片必须上传!");
        return;
    }

    //var des = $.trim($("#Description").val());
    //if (des == "" || des.length == 0) {
    //    alertMsg("描述不能为空!");
    //    return;
    //} else if (des.length < 10 || des.length > 100) {
    //    alertMsg("描述不能少于10个汉字或大于100字!!");
    //    return;
    //}
    
    var index = layer.load(0, { shade: false });
    var postData = $("#form1").GetWebControls();
    postData["Pic1"] = pic1;
    postData["Pic2"] = pic2;
    postData["Pic3"] = pic3;
    postData["Pic4"] = pic4;

    $.ajax({
        url: "/WeChatManage/Sikelai/SaveForm",
        type: "POST",
        data: postData,
        success: function (responseText) {
            layer.close(index);
            layer.alert(responseText, {
                icon: 1, skin: 'layui-layer-lan', closeBtn: 0
            }, function () {
                window.location.href = "/WeChatManage/Sikelai/SignUpCheck";
            });
        },
        error: function () {

        }
    });
}

function alertMsg(msg) {
    layer.alert(msg, {
        skin: 'layui-layer-molv', closeBtn: 0
    });
}

