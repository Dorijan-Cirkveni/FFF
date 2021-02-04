import React from "react";
import { Redirect } from "react-router-dom";

import Calendar from "../../components/shared/Calendar";

import { AuthConsumer } from "../../authContext";

const Kalendar = () => (
    <AuthConsumer>
        {({ authenticated, user , accessToken}) =>
            authenticated ? (
                <Calendar user={user} accessToken={accessToken} />

            ) : (
                    <Redirect to="/" />

                )}
    </AuthConsumer>
);

export default Kalendar;