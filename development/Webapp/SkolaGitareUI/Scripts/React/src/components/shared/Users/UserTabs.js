import React from "react";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import Paper from "@material-ui/core/Paper";

import RegistrationsSwitcher from "./RegistrationsSwitcher";
import UsersList from "./UserList";


import TabPanel from "../TabPanel";



function a11yProps(index) {
    return {
        id: `user-tab-${index}`,
        "aria-controls": `user-tabpanel-${index}`,
    };
}



export default function UserTabs(props) {
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <div>
                    <Paper square >
                        <Tabs
                            value={value}
                            onChange={handleChange}
                            indicatorColor="secondary"
                            textColor="secondary  "
                            aria-label="icon tabs example"
                            centered
                        >
                            <Tab
                                label="Učitelji"
                                aria-label="last"
                                {...a11yProps(0)}
                            />
                            <Tab
                                label="Učenici"
                                aria-label="search"
                                {...a11yProps(1)}
                            />
                            <Tab
                                label="Registracije"
                                aria-label="registrations"
                                {...a11yProps(2)}
                            />
                        </Tabs>
                    </Paper>
                    <TabPanel value={value} index={0}>
                <UsersList url="Members/Teachers" accessToken={props.accessToken} user={props.user}/>
                    </TabPanel>
                    <TabPanel value={value} index={1}>
                <UsersList url="Members/Students" accessToken={props.accessToken} user={props.user}/>

                    </TabPanel>
                    <TabPanel value={value} index={2}>
                <RegistrationsSwitcher accessToken={props.accessToken} user={props.user}/>
                </TabPanel>
            </div>
             
    );
}
