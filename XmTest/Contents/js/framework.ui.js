$.reload = function () {
    location.reload();
    return false;
}

$.loading = function (bool, text) {
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}


$.submitForm = function (options) {
    var defaults = {
        url: "",
        data: [],
        loading: "正在提交数据...",
        success: function (data) {
            if (data.code > 0) {
                alert(data.msg);
                console.log(data.msg);
                location.reload();
            }
            else {
                alert(data.msg);
            }
        },
        close: true
    };

    var params = $.extend(defaults, options);

    $.loading(true, params.loading);
    window.setTimeout(function () {
        $.ajax({
            url: params.url,
            data: params.data,
            type: "post",
            dataType: "json",
            success: params.success,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.loading(false);
            },
            beforeSend: function () {
                $.loading(true, params.loading);
            },
            complete: function () {
                $.loading(false);
            }
        })
    })


}


//序列化成Object类型
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};