var savearticlereorder = {
    init: function (saveurl) {
        this._save(saveurl);
    },
    _save: function (saveurl) {

        $('#ReorderContainerAction').on('click', '#btnSave ', function () {

            var SecID = encodeURI('SecID') + '=' + encodeURI($('#SecID').val());
            var Type = encodeURI('Type') + '=' + encodeURI($('#Type').val());
            var Articles = [];
            var Counter = 0;
            $('#sortable li').each(function (i) {

                var article = {};
                article['ArticleOrder[' + i + '].NewsID'] = $(this).attr('data-id');
                Articles.push(article);

                Counter++;
                if (Counter == 21) { return false; }
            });

            $.ajax({
                type: 'GET',
                url: saveurl,
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                dataType: 'json',
                data: miscFeatures.serializeJson(Articles) + '&' + SecID + '&' + Type,
                cache: false
            }).done(function () {
                swal('', 'تم الحفظ بنجاح', 'success');
            });

        });


        $('#ReorderContainerAction').on('click', '#btnAddNum', function () {
            var SecID = encodeURI($('#SecID').val());
            var Type = encodeURI($('#Type').val());
            var Num = encodeURI($('#txtNum').val().trim());

            var RetVal = null;
            if ((Num) && (Num != "0")) {
                $.ajax({
                    url: '/Article/GetArticleForReOrderByID',
                    data: { 'SecID': SecID, 'Type': Type, 'Num': Num },
                    // dataType: "Json",
                    success: function (result) {
                        RetVal = result;

                        if (result.indexOf('li') < 0) {

                            if (result == '0') {
                                swal('هذا الخبر مرتب فى التوب برجاء أعادة ترتيبة بنفسك');
                            }
                            else if (result == '-1') {
                                swal('هذا الخبر مرتب فى عاجل الدوار برجاء أعادة ترتيبة بنفسك');
                            }
                            else {
                                swal('هذا الخبر مرتب فى قسمه برجاء أعادة ترتيبة بنفسك');
                            }
                        }
                        else {
                            $('#sortable li:eq(0)').before(result);
                        }
                    },
                    async: false
                }).done(function (result) {
                    RetVal = result;
                    if (result.indexOf('li') < 0) {

                        if (result == '0') {
                            swal('هذا الخبر مرتب فى التوب برجاء أعادة ترتيبة بنفسك');
                        }
                        else if (result == '-1') {
                            swal('هذا الخبر مرتب فى عاجل الدوار برجاء أعادة ترتيبة بنفسك');
                        }
                        else {
                            swal('هذا الخبر مرتب فى قسمه برجاء أعادة ترتيبة بنفسك');
                        }

                    }
                    else {
                        var deleteditem = $('#sortable li:eq(0)').attr("data-id");
                        var length = $("ul").find('[data-id="' + deleteditem + '"]').length;
                        if (length == 2) {
                            $("ul").find("[data-id='" + deleteditem + "']").last().remove();
                        }
                        if (RetVal && RetVal.length > 3)
                            swal("", "تم  اضافة الخبر", "success");
                        else
                            swal("", "لم يتم اضافة الخبر", "warning");
                    }
                });
            }
            else {
                swal("", "برجاء وضع رقم", "warning");
            }
        });


    }
};

var articlereorder = {
    init: function (saveurl) {
        this._save(saveurl);
    },
    _save: function (saveurl) {

        $('#ReorderContainerAction').on('change', '#SecID', function () {
            var SecID = $(this).val();
            var Type = encodeURI($('#Type').val());

            $.ajax({
                url: saveurl,
                data: { 'SecID': SecID, 'Type': Type },
                // dataType: "Json",
                success: function (result) {
                    $('#ReorderContainer').html(result)
                    $('#sortable').sortable({
                        placeholder: 'ui-state-highlight'
                    });
                    $('#sortable').disableSelection();
                },
                async: false
            });
        });

        $('#ReorderContainerAction').on('change', '#Type', function () {
            var Type = $(this).val();

            //if (Type == 2)
            //    $('#SecID').val(87);

            var SecID = encodeURI($('#SecID').val());

            $.ajax({
                url: saveurl,
                data: { 'SecId': SecID, 'Type': Type },
                // dataType: "Json",
                success: function (result) {
                    $('#ReorderContainer').html(result)
                    $('#sortable').sortable({
                        placeholder: 'ui-state-highlight'
                    });
                    $('#sortable').disableSelection();
                },
                async: false
            });
        });

    }
};