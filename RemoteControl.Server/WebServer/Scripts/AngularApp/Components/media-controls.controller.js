window.MediaControls.controller('MediaControlsController', function($scope) {


    var userIsOpen = false;

    $scope.openToolbar = function() {
        userIsOpen = true;
    };

    $scope.$watch('isOpen', function(newVal, oldVal) {
        console.log(newVal);

        $scope.isOpen = userIsOpen;
    });

});