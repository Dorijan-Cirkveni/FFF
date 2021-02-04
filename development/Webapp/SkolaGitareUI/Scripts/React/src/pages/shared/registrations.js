import React from "react";
import { Redirect } from "react-router-dom";


import { AuthConsumer } from "../../authContext";

const Registrations = () => (
    <AuthConsumer>
        {({ authenticated, user }) =>
            authenticated ? (
                <div>
                    Registrations

                </div>
            ) : (
                    <Redirect to="/" />

                )}
    </AuthConsumer>
);

export default Registrations;