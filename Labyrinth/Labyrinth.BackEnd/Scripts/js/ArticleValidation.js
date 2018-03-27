function countTitle(what) {
    formcontent = what.form.Title.value;
    what.form.counttitle.value = formcontent.length;
    var ff = what.form.counttitle.value;
    if (ff > 70) {
        document.getElementById("counttitle").style.color = "red";
    }
    else {
        document.getElementById("counttitle").style.color = "green";
    }
}

function countBrief(what) {
    formcontent = what.form.Brief.value;
    what.form.countbrief.value = formcontent.length;
    var ff = what.form.countbrief.value;
    if (ff > 160) {
        document.getElementById("countbrief").style.color = "red";
    }
    else {
        document.getElementById("countbrief").style.color = "green";
    }
}



function countWrittenby(what) {
    formcontent = what.form.WrittenBy.value;
    what.form.countwrittenby.value = formcontent.length;
    var ff = what.form.countwrittenby.value;
    if (ff > 254) {
        document.getElementById("countwrittenby").style.color = "red";
    }
    else {
        document.getElementById("countwrittenby").style.color = "green";
    }
}