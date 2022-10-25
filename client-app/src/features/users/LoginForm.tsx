import React from "react";
import {ErrorMessage, Form, Formik} from "formik";
import * as Yup from 'yup';
import {Button, Header, Label} from "semantic-ui-react";
import MyTextInput from "../../app/common/form/MyTextInput";
import {observer} from "mobx-react-lite";
import {useStore} from "../../app/stores/store";

export default observer(function LoginForm() {
    const {userStore} = useStore();

    const validationSchema = Yup.object({
        email: Yup.string().required().email(),
        password: Yup.string().required()
    })

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={{email: '', password: '', error: null}}
            onSubmit={(values, {setErrors}) => userStore.login(values).catch(error =>
                setErrors({error: 'Invalid email or password'}))}
        >
            {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                <Form className={'ui form'} onSubmit={handleSubmit} autoComplete={'off'}>
                    <Header as={'h2'} content={'Login to Reactivities'} color={'teal'} textAlign={'center'}/>
                    <MyTextInput name={'email'} placeholder={'Email'} />
                    <MyTextInput name={'password'} placeholder={'Password'} type={'password'} />
                    <ErrorMessage
                        name={'error'} render={() =>
                        <Label basic color={'red'} style={{marginBottom: 10}} content={errors.error} />} />
                    <Button
                        disabled={isSubmitting || !isValid || !dirty}
                        loading={isSubmitting}
                        positive type={'submit'} content={'Login'} fluid
                    />
                </Form>
            )}
        </Formik>
    )
})