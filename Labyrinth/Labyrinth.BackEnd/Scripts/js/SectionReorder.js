var sectionreorder = {
    init: function (saveurl) {
        this._save(saveurl);
    },
    _save: function (saveurl) {
        $('#ReorderContainer').on('click', '#btnSave ', function () {
            var Sections = [];
            $("#sortable li").each(function (i) {
                var section = {};
                section["SectionOrder[" + i + "].Id"] = $(this).attr('data-id');
                Sections.push(section);
            });
            $.ajax({
                type: "GET",
                url: saveurl,
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                dataType: "json",
                data: miscFeatures.serializeJson(Sections),
                cache: false
            }).done(function () {
                swal("", "تم الحفظ بنجاح", "success");
            });
        });
    }
};
