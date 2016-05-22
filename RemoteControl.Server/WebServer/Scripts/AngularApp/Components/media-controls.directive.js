window.MediaControls.directive('rcMediaControls', function () {
    return{
        templateUrl: '/Scripts/AngularApp/Components/media-controls.view.html',
        controller: 'MediaControlsController',
        scope: {
            isOpen: '=isOpen'
        }
    };
})