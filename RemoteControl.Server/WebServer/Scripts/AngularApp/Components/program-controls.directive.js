window.MediaControls.directive('rcProgramControls', function () {
    return{
        templateUrl: '/Scripts/AngularApp/Components/program-controls.view.html',
        scope: {
            isOpen: '=isOpen'
        }
    };
})