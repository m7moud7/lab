var editorreorder = {
    init: function (saveurl) {
        this._save(saveurl);
    },
    _save: function (saveurl) {
        $('#ReorderContainer').on('click', '#btnSave ', function () {
            var Editors = [];
            $("#sortable li").each(function (i) {
                var editor = {};
                editor["EditorOrder[" + i + "].Id"] = $(this).attr('data-id');
                Editors.push(editor);
            });
            $.ajax({
                type: "GET",
                url: saveurl,
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                dataType: "json",
                data: miscFeatures.serializeJson(Editors),
                cache: false
            }).done(function () {
                swal("", "تم الحفظ بنجاح", "success");
            });
        });
    }
};
