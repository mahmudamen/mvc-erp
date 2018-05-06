var dt = $('#tbSysrep').DataTable(
    {
        bProcessing: false, sAjaxSource: '/ERP/Ag/systmrep', bFilter: false, bInfo: false, lengthChange: false, scrollY: '220px', scrollCollapse: true, bPaginate: false,

        footerCallback: function (row, data, start, end, display) {
            var api = this.api(), data;
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };
            TDebF = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            TCreF = api
              .column(3, { page: 'current' })
              .data()
              .reduce(function (a, b) {
                  return intVal(a) + intVal(b);
              }, 0);
            TDebCur = api
              .column(4, { page: 'current' })
              .data()
              .reduce(function (a, b) {
                  return intVal(a) + intVal(b);
              }, 0);
            TCreCur = api
              .column(5, { page: 'current' })
              .data()
              .reduce(function (a, b) {
                  return intVal(a) + intVal(b);
              }, 0);
            TDebLast = api
              .column(6, { page: 'current' })
              .data()
              .reduce(function (a, b) {
                  return intVal(a) + intVal(b);
              }, 0);
            TCreLast = api
              .column(7, { page: 'current' })
              .data()
              .reduce(function (a, b) {
                  return intVal(a) + intVal(b);
              }, 0);
            $(api.column(2).footer()).html(
                 TDebF
            );
            $(api.column(3).footer()).html(
                TCreF
           );
            $(api.column(4).footer()).html(
               TDebCur
          );
            $(api.column(5).footer()).html(
               TCreCur
          );
            $(api.column(6).footer()).html(
               TDebLast
          );
            $(api.column(7).footer()).html(
               TCreLast
          );
        },
        columns: [
    { "data": "SysID", "visible": false, "bSearchable": false },
    { "data": "SysName", "sClass": "dt-center", "width": "30%" },
    { "data": "DebF", "sClass": "dt-center", "width": "10%" },
    { "data": "CreF", "sClass": "dt-center", "width": "10%" },
    { "data": "DebCur", "sClass": "dt-center", "width": "10%" },
    { "data": "CreCur", "sClass": "dt-center", "width": "10%" },
    { "data": "DebLast", "sClass": "dt-center", "width": "10%" },
    { "data": "CreLast", "sClass": "dt-center", "width": "10%" }
        ]
    });
 
 
// Name of the filename when exported (except for extension)
var export_filename = 'Filename-' ;

// Configure Export Buttons
new $.fn.dataTable.Buttons(dt, {
    buttons: [
        {
            text: '<i class="glyphicon glyphicon-copy"></i>',
            extend: 'copy',
            className: 'btn btn-primary btn-sm m-5   pull-left'
        }, {
            text: '<i class="glyphicon glyphicon-floppy-saved"></i>',
            extend: 'csv',
            className: 'btn btn-primary btn-sm m-5   pull-left',
            title: export_filename,
            extension: '.csv'
        }, {
            text: '<i class="glyphicon glyphicon-floppy-save"></i>',
            extend: 'excel',
            className: 'btn btn-primary btn-sm m-5   pull-left',
            title: export_filename,
            extension: '.xls'
        }, {
            text: '<i class="glyphicon glyphicon-save"></i>',
            extend: 'pdf',
            className: 'btn btn-xs btn-primary m-5   pull-left',
            title: export_filename,
            extension: '.pdf'
        }
    ]
});

// Add the Export buttons to the toolbox
dt.buttons(0, null).container().appendTo('#butan');


// Configure Print Button
new $.fn.dataTable.Buttons(dt, {
    buttons: [
        {
            text: '<i class="glyphicon glyphicon-print"></i>',
            extend: 'print',
            className: 'btn btn-primary btn-sm m-5  pull-left'
        }
    ]
});

// Add the Print button to the toolbox
dt.buttons(1, null).container().appendTo('#butan');


// Select Buttons
new $.fn.dataTable.Buttons(dt, {
    buttons: [
        {
            text: '<i class="glyphicon glyphicon-ok-sign"></i>',
            extend: 'selectAll',
            className: 'btn btn-primary btn-sm m-5 pull-left'
        }, {
            text: '<i class="glyphicon glyphicon-remove-sign"></i>',
            extend: 'selectNone',
            className: 'btn btn-primary btn-sm m-5 pull-left'
        }
    ]
});

// Add the Select buttons to the toolbox
dt.buttons(2, null).container().appendTo('#butan');





