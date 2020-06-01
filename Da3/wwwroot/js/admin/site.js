var defaultExport = {
    orthogonal: 'export',
    columns: ':visible'
};

var defaultExcelButton = {
    extend: 'excelHtml5',
    exportOptions: defaultExport,
    filename: function () {
        return document.title + new Date().getTime();
    }
};

var defaultCSVButton = {
    extend: 'csvHtml5',
    exportOptions: defaultExport,
    filename: function () {
        return document.title + new Date().getTime();
    }
};

var copyEmailButton = {
    extend: 'copyHtml5',
    text: 'Copy emails',
    header: false,
    title: null,
    newline: ";",
    exportOptions: {
        columns: ["email:name"]
    }
};

var defaultButtons = [
    'pageLength',
    defaultExcelButton,
    defaultCSVButton
];

var copiedEmailLanguage = {
    buttons: {
        copyTitle: 'Copy all rows to clipboard',
        copySuccess: {
            _: '%d distinct emails copied',
            1: '1 distinct email copied'
        }
    }
};

function formatDate(str) {
    const date = new Date(str);
    const ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(date);
    const mo = new Intl.DateTimeFormat('en', { month: 'short' }).format(date);
    const da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(date);
    return `${da}-${mo}-${ye}`;
}
