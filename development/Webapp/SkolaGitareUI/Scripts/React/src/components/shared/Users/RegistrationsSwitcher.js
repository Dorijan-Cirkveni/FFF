import React from "react";
import { Redirect } from "react-router-dom";
import Registrations from "./Registrations";

function RegistrationsSwitcher(props) {
    switch (props.user.role) {

        case "Teacher":
            return < Registrations
                user={props.user}
                accessToken={props.accessToken}
                urlGet = 'Members/Students/Requests'/>;
        case "Admin":
            return <Registrations
                user={props.user}
                accessToken={props.accessToken}
                urlGet='Members/Teachers/Requests' 
            />;
        default:
            return <Redirect to="/" />;
    }
}

export default RegistrationsSwitcher;