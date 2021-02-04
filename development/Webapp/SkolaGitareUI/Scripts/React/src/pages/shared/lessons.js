import React from "react";
import { Redirect } from "react-router-dom";

import Lessons from "../../components/shared/Lessons";

import { AuthConsumer } from "../../authContext";



const LessonsPage = () => (
    <AuthConsumer>
        {({ authenticated, user, accessToken }) =>
            authenticated ? (
                <Lessons user={user} accessToken={accessToken}/>
            ) : (
                 <Redirect to="/" />

                )
        }
    </AuthConsumer>
);

export default LessonsPage;