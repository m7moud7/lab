var miscFeatures = {
    serializeJson: function (jsonObject) {
        var serializedString = "";
        for (item in jsonObject) {
            //debugger;
            var obj = jsonObject[item];
            for (var prop in obj) {
                var propName = encodeURI(prop);
                var propValue = encodeURI(obj[prop]);
                serializedString += "&" + propName + "=" + propValue;
            }
        }
        return serializedString;
    }
};
//to filter dropdowns
function cascadeDDL(ddl, source, data) {
    $.ajax({
        type: "POST",
        url: source,
        data: data,
        dataType: "Json",
        success: function (result) {
            //
            $(ddl).FillDropdown(result);
        },
        async: false
    });
}
$.fn.FillDropdown = function (data) {
    return this.clearSelect().each(function () {
        // 
        if (this.tagName !== 'SELECT')
            return;
        var dropdownList = $(this);
        $.each(data, function (index, optionData) {
            var option = $('<option ' + ((optionData.Selected) ? 'Selected' : '') + '>' + optionData.Id + '</option>').val(optionData.Id).html(optionData.Name);
            dropdownList.append(option);
        });
    });
};
$.fn.clearSelect = function () {
    return this.each(function () {
        if (this.tagName == 'SELECT')
            this.options.length = 0;
    });
};
//# sourceMappingURL=jquery.extension.js.map 
//# sourceMappingURL=jquery.extension.js.map 
//# sourceMappingURL=jquery.extension.js.map 
//# sourceMappingURL=jquery.extension.js.map 
//# sourceMappingURL=jquery.extension.js.map 
//# sourceMappingURL=jquery.extension.js.map