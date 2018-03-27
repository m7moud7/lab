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
                width: 150,
                height: 150,
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
        var $uploadCrop;

        function readFile(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.upload-demo').addClass('ready');
                    $uploadCrop.croppie('bind', {
                        url: e.target.result
                    }).then(function () {
                        console.log('jQuery bind complete');
                    });
                }
                reader.readAsDataURL(input.files[0]);
            }
            else {
                swal("Sorry - you're browser doesn't support the FileReader API");
            }
        }

        $uploadCrop = $('#upload-demo').croppie({
            viewport: {
                width: 100,
                height: 100,
            },
            enableExif: true
        });

        $('#upload').on('change', function () { readFile(this); });

        $('.upload-result').on('click', function (ev) {
            $uploadCrop.croppie('result', {
                type: 'canvas',
                size: 'viewport'
            }).then(function (resp) {
                popupResult({
                    src: resp
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
