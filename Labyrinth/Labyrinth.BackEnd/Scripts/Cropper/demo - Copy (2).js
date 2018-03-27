var Demo = (function () {

    function output(node) {
        var existing = $('#result .croppie-result');
        if (existing.length > 0) {
            existing[0].parentNode.replaceChild(node, existing[0]);
        }
        else {
            $('#result')[0].appendChild(node);
        }
    }

    function popupResult(result) {
        var html;
        if (result.html) {
            html = result.html;
        }
        if (result.src) {
            html = '<img src="' + result.src + '" />';
        }
        swal({
            title: '',
            html: true,
            text: html,
            allowOutsideClick: true
        });
        setTimeout(function () {
            $('.sweet-alert').css('margin', function () {
                var top = -1 * ($(this).height() / 2),
                    left = -1 * ($(this).width() / 2);

                return top + 'px 0 0 ' + left + 'px';
            });
        }, 1);
    }

    function demoMain() {
        var mc = $('#cropper-1');
        mc.croppie({
            viewport: {
                width: 600,
                height: 400,
            },
            boundary: {
                width: 300,
                height: 300
            },
            // url: 'demo/demo-1.jpg',
            // enforceBoundary: false
            // mouseWheelZoom: false
        });
        mc.on('update.croppie', function (ev, data) {
            // console.log('jquery update', ev, data);
        });
        $('.js-main-image').on('click', function (ev) {
            mc.croppie('result', {
                type: 'rawcanvas',
                // size: { width: 300, height: 300 },
                format: 'png'
            }).then(function (canvas) {
                popupResult({
                    src: canvas.toDataURL()
                });
            });
        });
    }

    function demoUpload() {
        var teest
        function readFile(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.upload-demo').addClass('ready');
                    for (i = 1; i < $("#imagecount").val() + 1; i++) {

                        //alert($("#divShow_" + i).find("#CurrentWidth" + i).val());
                        var d1 = $("#divShow_" + i).find("#CurrentWidth" + i).val();
                        var d2 = $("#divShow_" + i).find("#CurrentHeight" + i).val();

                        var retio = d1 / d2;
                        var num = Math.round(200 / retio);
                        $teest = $('#upload-demo_' + i).croppie({
                            enableExif: true,
                            viewport: {
                                width: 200,
                                height: num,
                                //type: 'circle',
                            },
                            enableOrientation: true,
                            //enforceBoundary: false,
                            enableExif: true
                        });



                        //teest.croppie('bind', {
                        //    url: '/images/1.jpg',
                        //  //  points: [77, 469, 280, 739]
                        //});

                        $teest.croppie('bind', {
                            url: e.target.result
                        }).then(function () {
                            for (i = 1; i < $("#imagecount").val() + 1; i++) {
                                $('#upload-demo_' + i).croppie('setZoom', 0);
                            }
                        });

                        //$('#upload-demo_' + i).croppie('setZoom', 0);

                        if ($("#imagecount").val() == i) {
                            $("#testmain").val(reader.result);
                            break;
                        }

                        $teest.destr
                    }
                }
                reader.readAsDataURL(input.files[0]);
            }
            else {
                // swal("Sorry - you're browser doesn't support the FileReader API");
            }
        }

        //$.ajaxSetup({
        //    headers: {
        //        'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        //    }
        //});

        $('#upload').on('change', function () { readFile(this); });

        $('.upload-result').on('click', function (ev) {
            var counter = 0;
            var formData = new FormData();
            $(".upload-msg1").each(function (index, value) {

                var d1 = $("#divShow_" + (index + 1)).find("#CurrentWidth" + (index + 1)).val();
                var d2 = $("#divShow_" + (index + 1)).find("#CurrentHeight" + (index + 1)).val();

                ///var retio = d1 / d2;
                ///var num = Math.round(200 / retio);

                $('#upload-demo_' + (index + 1)).croppie('result', {
                    type: 'base64',
                    size: { width: d1, height: d2 },
                    backgroundColor: 'white'
                }).then(function (resp) {
                    formData.append('Attach' + counter, resp);
                    formData.append('SetKey_' + counter, $("#SetKey_" + (index + 1)).val());
                    formData.append('Val1_' + counter, $("#Val1_" + (index + 1)).val());
                    counter++;
                    if (counter == $("#imagecount").val()) {
                        formData.append('Caption', "testetset");
                        formData.append('Main', $("#testmain").val());
                        $.ajax({
                            type: 'POST',
                            url: "/Attachment/Add1",
                            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                            dataType: "json",
                            data: formData,
                            contentType: false,
                            processData: false,
                            //async: false,
                            cache: false,
                            error: function (xhr, httpStatusMessage, customErrorMessage) {
                                if (xhr.status === 410) {
                                    alert(customErrorMessage);
                                }
                            }
                        }).done(function (MSG) {
                            swal("", "تم الحفظ بنجاح", "success");
                        });
                    }
                });
            });
        });
    }

    function init() {
        demoMain();
        demoUpload();
    }

    return {
        init: init
    };
})();
