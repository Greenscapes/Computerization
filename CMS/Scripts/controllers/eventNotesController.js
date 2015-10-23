function EventNotesController( $scope, $resource, $routeParams, $location, Modal ) {
    var eventNotesResource = $resource( '/api/eventnotes/:eventid', { eventid: 1 },
    {
        'update': { method: 'PUT' }
    } );
    var eventNoteResource = $resource( '/api/eventnotes/:eventid' );
    var eventschedulesResource = $resource( '/api/eventschedules' );
    var eventscheduleResource = $resource( '/api/eventschedules/:eventscheduleId',
{ eventscheduleId: $routeParams.eventid },
{
        'update': { method: 'PUT' }
});

    var eventNotebyScheduleResource = $resource( '/api/eventnotes/:eventscheduleId', { eventscheduleId: $routeParams.eventid }
        );
    $scope.eventid = $routeParams.eventid;
    $scope.eventSchedules = eventschedulesResource.query( function () {
        var element = $( "#grid" ).kendoGrid( {
            dataSource: {
                data: $scope.eventSchedules,
                pageSize: 6,
                serverPaging: true,
                serverSorting: true
            },
            editable: "inline",
            height: 600,
            sortable: true,
            pageable: true,
            detailInit: detailInit,
            dataBound: function () {
                this.expandRow( this.tbody.find( "tr.k-master-row" ).first() );
            },
            columns: [
                {
                    field: "Title",
                    title: "Event Title",
                    width: "110px",
                    type: "string",
                    editable: false,
                },
                {
                    field: "Description",
                    title: "Description",
                    width: "110px",
                    type: "string",
                    editable: false,
                },
             
                
                {
                    field: "StartTime",
                    title: "Start Time",
                    type: "date",
                    width: "110px",
                    format: "{0:dd-MMM-yyyy hh:mm:ss tt}",
                    parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"],
                    editable: false,
                },
                {
                    field: "EndTime",
                    title: "End Time",
                    type: "date",
                    width: "110px",
                    format: "{0:dd-MMM-yyyy hh:mm:ss tt}",
                    parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"],
                    editable: false,
                },
                 {
                     field: "ActualStartTime",
                     title: "Actual Start Time",
                     type: "date",
                     width: "110px",
                     format: "{0:dd-MMM-yyyy hh:mm:ss tt}",
                     parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
                 },
                 {
                     field: "ActualEndTime",
                     title: "Actual End Time",
                     type: "date",
                     width: "110px",
                     format: "{0:dd-MMM-yyyy hh:mm:ss tt}",
                     parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
                 },
                    {
                        field: "Status",
                        type: "number",
                        title: "Status",
                        width: "110px",
                        template: "#if( Status==0){#<text class='templateCell'>New</text>#} else if( Status==1) {#<text class='templateCell'>InProgress</text>#} else if( Status==2) {#<text'class='templateCell'>Completed</text>#} else if( Status==3){#<text class='templateCell'>Closed</text>#}#",
                    },
                      { command: ["edit"], title: "&nbsp;", width: "250px" },
                
            ],
            edit: function (e) {
              
            },
            update: function ( e ) {
                
            },
            change: function ( e ) {
               
            },
            save: function ( e ) {
                var event = e.model;
                var eventSchedule = new Object( {
                    Id: event.Id,
                    StartTime: event.StartTime,
                    EndTime: event.EndTime,
                    Title: event.Title,
                    Description: event.Description,
                    IsAllDay: event.IsAllDay,
                    RecurrenceRule: event.RecurrenceRule,
                    RecurrenceID: event.RecurrenceID,
                    RecurrenceException: event.RecurrenceException,
                    StartTimezone: event.StartTimezone,
                    EndTimezone: event.EndTimezone,
                    Status: event.Status,
                    ActualStartTime: event.ActualStartTime,
                    ActualEndTime: event.ActualEndTime,
                    PropertyTaskId: event.PropertyTaskId,

                } )
                eventscheduleResource.update( { eventscheduleId: eventSchedule.Id }, eventSchedule, function () { } );
                
            },
        } );

    } );
   
 
    var StatusEnumValues = [
          { Id: 0, Description: "New" },
          { Id: 1, Description: "InProgress" },
                    { Id: 2, Description: "Completed" },
                              { Id: 0, Description: "Closed" }

    ];

    
        
        function detailInit( e ) {



        $scope.propertyTaskEventNotes = eventNotebyScheduleResource.query( { eventscheduleId: e.data.Id }, function () {
            $( "<div/>" ).appendTo( e.detailCell ).kendoGrid( {
                dataSource: {
                    data:  $scope.propertyTaskEventNotes,
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    pageSize: 10,
                    filter: { field: "EventScheduleId", operator: "eq", value: e.data.Id }
                },
                scrollable: false,
                sortable: true,
                pageable: true,
                toolbar: [{ name: "create", text: "Add Note" }],
                editable: "inline",
                columns: [
                    
                    { field: "Notes", title: "Notes", width: "110px", },
                    {
                        field: "ReviewStatus",
                        title: "Review Status",
                        width: "110px",
                        template: "#if( ReviewStatus==0){#<text class='templateCell'>New</text>#} else if( ReviewStatus==1) {#<text class='templateCell'>InReview</text>#} else if( ReviewStatus==2) {#<text class='templateCell'>Reviewed</text>#} else if( ReviewStatus==3){#<text class='templateCell'>Closed</text>#}#",

                    },
                 { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }
                ],
                edit: function (e) {
                    
                },
                update: function ( e ) {
                
                },
                change: function ( e ) {
               
                },
                save: function ( e ) {
                    var eventNote = e.model;
                    var propertyeventNote = new Object( {
                        Id :0,
                        Notes: eventNote.Notes,
                        ReviewStatus: eventNote.ReviewStatus,
                        EventScheduleId: eventNote.EventScheduleId,

                    } )
                    if ( eventNote.Id ) {
                        propertyeventNote.Id = eventNote.Id
                        eventNotesResource.update( { eventid: propertyeventNote.Id }, propertyeventNote, function () { } );
                    }
                    else {

                        eventNoteResource.save( propertyeventNote );
                    }
                 
                
                },
            } );
        } );
    }


    $scope.back = function () {
        //$location.path( "/customerroutes" );
        //if ( !$scope.$$phase ) $scope.$apply();
    };
    
   function categoryDropDownEditor(container, options) {
                    $('<input required data-text-field="CategoryName" data-value-field="CategoryID" data-bind="value:' + options.field + '"/>')
                        .appendTo(container)
                        .kendoDropDownList({
                            autoBind: false,
                            dataSource: {
                                type: "odata",
                                transport: {
                                    read: "//demos.telerik.com/kendo-ui/service/Northwind.svc/Categories"
                                }
                            }
                        });
                }
    
}

EventNotesController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'EventNotesController', EventNotesController );