import React from "react";
import { Redirect } from "react-router-dom";

import Users from "../../components/shared/Users";

import { AuthConsumer } from "../../authContext";



const LessonsPage = () => (
    <AuthConsumer>
        {({ authenticated, user, accessToken }) =>
            authenticated ? (
                <Users user={user} accessToken={accessToken} />
            ) : (
                    <Redirect to="/" />

                )
        }
    </AuthConsumer>
);

export default LessonsPage;