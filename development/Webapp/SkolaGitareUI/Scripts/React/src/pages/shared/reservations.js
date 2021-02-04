import React from "react";
import { Redirect } from "react-router-dom";


import { AuthConsumer } from "../../authContext";

const Reservations = () => (
    <AuthConsumer>
        {({ authenticated, user }) =>
            authenticated ? (
                <div>
                    Rezervacije

                </div>
            ) : (
                    <Redirect to="/" />

                )}
    </AuthConsumer>
);

export default Reservations;